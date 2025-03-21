using AutoMapper;
using FarmPulse.API.DTOs;
using FarmPulse.Core.Interfaces;
using FarmPulse.Core;
using FarmPulse.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FarmPulse.API.Errors;

namespace FarmPulse.API.Controllers
{
	/// <summary>
	/// Handles operations related to chickens and their weights.
	/// Provides endpoints for adding new chickens, updating weights, and managing activity status.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class CreateChickenController : ControllerBase
	{

		private readonly IChickenRepository _chickenRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWeightRepository _weightRepository;
		/// <summary>
		/// Constructor for CreateChickenController to inject dependencies.
		/// </summary>
		/// <param name="chickenRepository">Repository for chicken data.</param>
		/// <param name="mapper">AutoMapper for DTO and model mapping.</param>
		/// <param name="unitOfWork">Unit of work for transactional data operations.</param>
		/// <param name="weightRepository">Repository for weight data.</param>
		public CreateChickenController(IChickenRepository chickenRepository, IMapper mapper, IUnitOfWork unitOfWork, IWeightRepository weightRepository)
		{
			_chickenRepository = chickenRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_weightRepository = weightRepository;
		}



		/// <summary>
		/// Adds a new chicken with weight information or updates existing chicken weight.
		/// </summary>
		/// <param name="dto">Chicken input data transfer object.</param>
		/// <returns>
		/// Returns the added chicken data with status 201 if successful.
		/// Returns appropriate error messages and status codes if there are failures.
		/// </returns>
		/// <response code="201">Chicken added or updated successfully.</response>
		/// <response code="400">Bad request if chicken can't be saved or updated.</response>
		/// <response code="500">Internal server error for unexpected failures.</response>
		[ProducesResponseType(typeof(ChickenInputDto), StatusCodes.Status201Created)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ApiExceptionResponse), StatusCodes.Status500InternalServerError)]
		[HttpPost("AddOrUpdateChickenWithWeight")]
		public async Task<ActionResult<ChickenInputDto>?> AddOrUpdateChickenWithWeight(ChickenInputDto dto)
		{
			var existingChicken = await _chickenRepository.GetByRFIDAsync(dto.RFID);
			var chickenRepo = _unitOfWork.Repository<Chicken>();
			var weightRepo = _unitOfWork.Repository<Weight>();
			var notificationRepo = _unitOfWork.Repository<Notification>();

			try
			{
				if (existingChicken == null)
				{

					#region Add New Chicken
					var newChicken = _mapper.Map<Chicken>(dto);
					await chickenRepo.AddAsync(newChicken);

					int SaveChicken = await _unitOfWork.SaveAsync();
					if (SaveChicken <= 0) return BadRequest(new ApiRespons(400, "Failed to save Chicken"));
					#endregion

					#region Add New Weight
					var newWeight = new Weight
					{
						ChickenId = newChicken.ChickenId,
						EntryWeight = newChicken.CurrentWeight,
						EntryTime = DateTime.Now
					};
					await weightRepo.AddAsync(newWeight);
					#endregion

					#region Add Notification Chicken Added
					var notification = new Notification
					{
						Message = $"New chicken added with RFID {newChicken.RFID}.",
						ChickenId = newChicken.ChickenId,
						IsRead = false
					};
					await notificationRepo.AddAsync(notification);
					#endregion

					#region Return Status 
					int SaveWeight = await _unitOfWork.SaveAsync();
					if (SaveWeight <= 0) return BadRequest(new ApiRespons(400, "Failed to save Weight"));

					return StatusCode(StatusCodes.Status201Created, dto);
					#endregion
				}
				else
				{
					#region Validation of ActivityStatus
					if (existingChicken.ActivityStatus == ActivityStatus.Sick.ToString())
					{
						return BadRequest(new ApiRespons(400, "Cannot update data for a sick chicken."));
					}
					else if (existingChicken.ActivityStatus == ActivityStatus.Dead.ToString())
					{

						return BadRequest(new ApiRespons(400, "Cannot update data for a Dead chicken."));
					}
					#endregion

					#region Update Weight For Chicken Table
					existingChicken.CurrentWeight = dto.Weight;
					chickenRepo.Update(existingChicken);
					#endregion

					#region Update Weight For Weight Table
					var existingWeight = await _weightRepository.GetExitWeightByChickenIdAsync(existingChicken.ChickenId);
					if (existingWeight != null && existingWeight.EntryTime.Date == DateTime.Now.Date)
					{
						existingWeight.ExitWeight = existingChicken.CurrentWeight;
						existingWeight.ExitTime = DateTime.Now;
						weightRepo.Update(existingWeight);

					} 
					else
					{
						var newWeight = new Weight
						{
							ChickenId = existingChicken.ChickenId,
							EntryWeight = existingChicken.CurrentWeight,
							EntryTime = DateTime.Now
						};

						await weightRepo.AddAsync(newWeight);
					}
					#endregion

					#region Update Weight by Difference Between ExitWeight & EntryWeight
					var weightDifference = existingWeight.ExitWeight - (existingWeight?.EntryWeight ?? 0);
					existingChicken.ActivityStatus = (weightDifference < 30) ? ActivityStatus.LowWeight.ToString() : ActivityStatus.Alive.ToString();
					chickenRepo.Update(existingChicken); 

					int saveStatus = await _unitOfWork.SaveAsync();
					if (saveStatus <= 0) return BadRequest(new ApiRespons(400, "Failed to save Activity Status"));
					#endregion

					#region Sent Notification about Curruntly Status
					if (existingChicken.ActivityStatus == ActivityStatus.LowWeight.ToString())
					{
						var existingNotification = await notificationRepo.GetFirstOrDefaultAsync(n =>
							n.ChickenId == existingChicken.ChickenId &&
							n.Message.Contains(existingChicken.ActivityStatus) &&
							n.CreatedAt.Date == DateTime.Now.Date);

						if (existingNotification == null)
						{
							await notificationRepo.AddAsync(new Notification
							{
								Message = $" Chicken with RFID {existingChicken.RFID} has {existingChicken.ActivityStatus}!",
								ChickenId = existingChicken.ChickenId,
								IsRead = false
							});

							int SaveStatus = await _unitOfWork.SaveAsync();
							if (SaveStatus <= 0) return BadRequest(new ApiRespons(400, "Failed to save Activity Status"));
						}
					} 
					#endregion
					return StatusCode(StatusCodes.Status201Created);
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiExceptionResponse(500, "An unexpected error occurred", ex.InnerException?.Message ?? ex.Message));
			}
		}
	}
}
