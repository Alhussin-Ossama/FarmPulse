using FarmPulse.Core.Models;

namespace FarmPulse.API.DTOs
{
	/// <summary>
	/// Data Transfer Object for returning chicken details to the client.
	/// </summary>
	public class ChickenToReturnDto
	{
		/// <summary>
		/// RFID tag associated with the chicken (used for identification).
		/// </summary>
		public string RFID { get; set; }

		/// <summary>
		/// The current weight of the chicken in grams.
		/// </summary>
		public double CurrentWeight { get; set; }

		/// <summary>
		/// The current activity status of the chicken (e.g., Alive, Sick, LowWeight, Dead).
		/// </summary>
		public string ActivityStatus { get; set; }

		/// <summary>
		/// The date of birth of the chicken. Defaults to the current date if not specified.
		/// </summary>
		public DateTime DateOfBirth { get; set; } = DateTime.Now;
	}
}
