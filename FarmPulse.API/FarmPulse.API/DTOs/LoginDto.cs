using System.ComponentModel.DataAnnotations;

namespace FarmPulse.API.DTOs
{
	/// <summary>
	/// Data Transfer Object for user login credentials.
	/// </summary>
	public class LoginDto
	{
		/// <summary>
		/// The email address associated with the user account.
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		/// <summary>
		/// The password for the user account.
		/// </summary>
		[Required]
		public string Password { get; set; }
	}
}
