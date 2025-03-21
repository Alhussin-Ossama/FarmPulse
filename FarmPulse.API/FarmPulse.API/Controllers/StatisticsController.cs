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
	/// Provides endpoints to retrieve statistical data about chickens, including average weights and mortality/survival rates over different time periods.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class StatisticsController : ControllerBase
	{
		private readonly IStatisticsRepository _statisticsRepository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="StatisticsController"/> class.
		/// </summary>
		/// <param name="statisticsRepository">The repository handling statistics data retrieval.</param>
		/// <param name="mapper">AutoMapper instance for mapping entities to DTOs.</param>
		public StatisticsController(IStatisticsRepository statisticsRepository, IMapper mapper)
		{
			_statisticsRepository = statisticsRepository;
			_mapper = mapper;
		}


		/// <summary>
		/// Retrieves statistics (average weights, mortality rate, survival rate) based on the specified period.
		/// </summary>
		/// <param name="period">The time period for the statistics. Possible values: "daily", "weekly", "monthly", "yearly".</param>
		/// <remarks>
		/// Example request:
		/// GET /api/Statistics/GetStatistics/daily
		/// </remarks>
		/// <returns>A list of statistics data for the requested period.</returns>
		/// <response code="200">Returns the statistics data successfully.</response>
		/// <response code="400">Invalid period value provided.</response>
		/// <response code="500">Internal server error while retrieving statistics.</response>
		[HttpGet("GetStatistics/{period}")]
		[ProducesResponseType(typeof(IEnumerable<StatisticsDto>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiRespons), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ApiExceptionResponse), StatusCodes.Status500InternalServerError)]
		public async Task<ActionResult<IEnumerable<StatisticsDto>>> GetStatistics(string period)
		{
			var statistics = await _statisticsRepository.GetStatisticsAsync(period);
			var statisticsDto = _mapper.Map<IEnumerable<StatisticsDto>>(statistics);
			return Ok(statisticsDto);
		}
	}
}
