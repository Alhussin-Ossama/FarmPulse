using System.ComponentModel.DataAnnotations;

namespace FarmPulse.API.DTOs
{
	/// <summary>
	/// Data Transfer Object for inputting chicken data (RFID and weight).
	/// Used when adding or updating chicken information.
	/// </summary>
	public class ChickenInputDto
	{
		/// <summary>
		/// RFID tag associated with the chicken (used for identification).
		/// </summary>
		[Required(ErrorMessage = "RFID is required.")]
		public string RFID { get; set; }

		/// <summary>
		/// Current weight of the chicken in grams.
		/// </summary>
		[Required(ErrorMessage = "Weight is required.")]
		public double Weight { get; set; }
	}
}
