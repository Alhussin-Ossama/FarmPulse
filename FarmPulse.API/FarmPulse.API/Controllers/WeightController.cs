using AutoMapper;
using FarmPulse.API.DTOs;
using FarmPulse.API.Errors;
using FarmPulse.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmPulse.API.Controllers
{
	/// <summary>
	/// Manages operations related to chicken weight history
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class WeightController : ControllerBase
	{
		private readonly IWeightRepository _weightRepository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="WeightController"/> class.
		/// </summary>
		/// <param name="weightRepository">Repository for handling weight data.</param>
		/// <param name="mapper">Mapper to convert entities to DTOs.</param>
		public WeightController(IWeightRepository weightRepository, IMapper mapper)
		{
			_weightRepository = weightRepository;
			_mapper = mapper;
		}

		/// <summary>
		/// Retrieves the weight history for a chicken by its RFID.
		/// </summary>
		/// <param name="RFID">The RFID of the chicken.</param>
		/// <param name="pageNumber">Page number for pagination (default is 1).</param>
		/// <param name="pageSize">Number of records per page (default is 10).</param>
		/// <returns>A paginated list of weight history entries.</returns>
		/// <response code="200">Returns the weight history list.</response>
		/// <response code="404">If the chicken with the given RFID is not found.</response>
		/// <response code="500">If an internal server error occurs.</response>
		[HttpGet("WeightHistory")]
		[ProducesResponseType(typeof(IEnumerable<WeighToReturnDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status404NotFound)]
		[ProducesResponseType(typeof(ApiExceptionResponse), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<WeighToReturnDto>>> GetWeightHistory(string RFID, int pageNumber = 1, int pageSize = 10)
		{
			try
			{
				var weightHistory = await _weightRepository.GetWeightHistoryByRFIDAsync(RFID);
				if (weightHistory == null || !weightHistory.Any())
				{
					return NotFound(new ApiRespons(404, "Chicken not found"));
				}
				var paginatedWeights = weightHistory.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

				var weightDtoList = _mapper.Map<IEnumerable<WeighToReturnDto>>(paginatedWeights);
				return Ok(weightDtoList);
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiExceptionResponse(500, "Error fetching weight history", ex.Message));
			}
		}
	}
}
