using FarmPulse.Core.Models;

namespace FarmPulse.API.DTOs
{
	/// <summary>
	/// Data Transfer Object representing statistical data of the chickens.
	/// </summary>
	public class StatisticsDto
	{
		/// <summary>
		/// The date for which the statistics are calculated.
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// The average weight of all chickens on the given date.
		/// </summary>
		public double AverageWeight { get; set; }

		/// <summary>
		/// The mortality rate of chickens (percentage of chickens that died).
		/// </summary>
		public double MortalityRate { get; set; }

		/// <summary>
		/// The survival rate of chickens (percentage of chickens that survived).
		/// </summary>
		public double SurvivalRate { get; set; }
	}
}
