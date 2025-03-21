using AutoMapper;
using FarmPulse.API.DTOs;
using FarmPulse.API.Errors;
using FarmPulse.Core;
using FarmPulse.Core.Interfaces;
using FarmPulse.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmPulse.API.Controllers
{
	/// <summary>
	/// Controller responsible for managing chickens, including retrieving, updating, and deleting chicken data.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ChickenController : ControllerBase
	{
		private readonly IChickenRepository _chickenRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public ChickenController(IChickenRepository chickenRepository, IMapper mapper, IUnitOfWork unitOfWork)
		{
			_chickenRepository = chickenRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Retrieves all chickens with their details.
		/// </summary>
		/// <remarks>Returns a list of all chickens stored in the system.</remarks>
		/// <response code="200">Returns the list of chickens</response>
		/// <response code="404">No chickens found</response>
		[HttpGet("GetAllChickens")]
		[ProducesResponseType(typeof(IEnumerable<ChickenToReturnDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ChickenToReturnDto>>> GetAllChickens()
		{
			var chickens = await _unitOfWork.Repository<Chicken>().GetAllAsync();
			if (chickens == null || !chickens.Any())
				return NotFound(new ApiRespons(404, "No chickens found"));

			return Ok(_mapper.Map<IEnumerable<ChickenToReturnDto>>(chickens));
		}

		/// <summary>
		/// Retrieves chickens by their activity status.
		/// </summary>
		/// <param name="status">The activity status (Alive, Sick, LowWeight, Dead).</param>
		/// <response code="200">Returns the filtered list of chickens</response>
		/// <response code="400">Invalid status provided</response>
		/// <response code="404">No chickens found with the given status</response>
		[HttpGet("GetChickensByStatus/{status}")]
		[ProducesResponseType(typeof(IEnumerable<ChickenToReturnDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<IEnumerable<ChickenToReturnDto>>> GetChickensByStatus(string status)
		{
			if (!Enum.TryParse<ActivityStatus>(status, true, out var parsedStatus))
				return BadRequest(new ApiRespons(400, "Invalid activity status"));

			var chickens = await _chickenRepository.GetFilteredChickens(c => c.ActivityStatus == parsedStatus.ToString());
			if (!chickens.Any())
				return NotFound(new ApiRespons(404, $"No chickens with status {parsedStatus} found"));

			return Ok(_mapper.Map<IEnumerable<ChickenToReturnDto>>(chickens));
		}

		/// <summary>
		/// Retrieves a chicken by its RFID.
		/// </summary>
		/// <param name="RFID">The RFID tag of the chicken.</param>
		/// <response code="200">Returns the chicken details</response>
		/// <response code="404">Chicken not found</response>
		[HttpGet("GetChickenByRFID/{RFID}")]
		[ProducesResponseType(typeof(ChickenToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ChickenToReturnDto>> GetByRfid(string RFID)
		{
			var chicken = await _chickenRepository.GetByRFIDAsync(RFID);
			if (chicken == null)
				return NotFound(new ApiRespons(404, $"Chicken with RFID {RFID} not found"));

			return Ok(_mapper.Map<ChickenToReturnDto>(chicken));
		}

		/// <summary>
		/// Retrieves the count of alive chickens.
		/// </summary>
		/// <response code="200">Returns the count of alive chickens</response>
		[HttpGet("AliveChickenCount")]
		[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
		public async Task<ActionResult<int>> GetAliveChickenCount()
		{
			int count = await _chickenRepository.GetAliveChickenCountAsync();
			return Ok(count);
		}

		/// <summary>
		/// Retrieves the count of dead chickens.
		/// </summary>
		/// <response code="200">Returns the count of dead chickens</response>
		[HttpGet("DeadChickenCount")]
		[ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
		public async Task<ActionResult<int>> GetDeadChickenCount()
		{
			int count = await _chickenRepository.GetDeadChickenCountAsync();
			return Ok(count);
		}

		/// <summary>
		/// Updates the activity status of a chicken (excluding 'Dead').
		/// </summary>
		/// <param name="RFID">The RFID tag of the chicken.</param>
		/// <param name="newStatus">The new activity status.</param>
		/// <response code="202">Status updated successfully</response>
		/// <response code="400">Invalid status or attempt to set status to 'Dead'</response>
		/// <response code="404">Chicken not found</response>
		[HttpPut("{RFID}/{newStatus}")]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status202Accepted)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> SetActivityStatusByUser(string RFID, string newStatus)
		{
			var chicken = await _chickenRepository.GetByRFIDAsync(RFID);
			if (chicken == null)
				return NotFound(new ApiRespons(404, "Chicken not found"));

			if (!Enum.TryParse<ActivityStatus>(newStatus, true, out var parsedStatus))
				return BadRequest(new ApiRespons(400, "Invalid activity status"));

			if (parsedStatus == ActivityStatus.Dead)
				return BadRequest(new ApiRespons(400, "Cannot set status to 'Dead' manually"));

			chicken.ActivityStatus = parsedStatus.ToString();
			await _unitOfWork.SaveAsync();
			return Accepted(new ApiRespons(202, "Status updated successfully"));
		}

		/// <summary>
		/// Marks a chicken as dead.
		/// </summary>
		/// <param name="RFID">The RFID tag of the chicken.</param>
		/// <response code="202">Chicken marked as dead</response>
		/// <response code="404">Chicken not found</response>
		[HttpPut("{RFID}/SetDead")]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status202Accepted)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> MarkChickenAsDead(string RFID)
		{
			var chicken = await _chickenRepository.GetByRFIDAsync(RFID);
			if (chicken == null)
				return NotFound(new ApiRespons(404, "Chicken not found"));

			chicken.ActivityStatus = ActivityStatus.Dead.ToString();
			await _unitOfWork.SaveAsync();
			return Accepted(new ApiRespons(202, "Chicken marked as dead"));
		}

		/// <summary>
		/// Deletes a chicken record from the database.
		/// </summary>
		/// <param name="RFID">The RFID tag of the chicken.</param>
		/// <response code="202">Chicken deleted successfully</response>
		/// <response code="404">Chicken not found</response>
		[HttpDelete("{RFID}")]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status202Accepted)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		public async Task<ActionResult> DeleteChicken(string RFID)
		{
			var chicken = await _chickenRepository.GetByRFIDAsync(RFID);
			if (chicken == null)
				return NotFound(new ApiRespons(404, "Chicken not found"));

			_unitOfWork.Repository<Chicken>().Delete(chicken);
			await _unitOfWork.SaveAsync();
			return Accepted(new ApiRespons(202, "Chicken deleted successfully"));
		}
	}
}

