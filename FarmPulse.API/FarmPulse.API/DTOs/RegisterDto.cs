using System.ComponentModel.DataAnnotations;

namespace FarmPulse.API.DTOs
{
	/// <summary>
	/// Data Transfer Object used for user registration.
	/// </summary>
	public class RegisterDto
	{
		/// <summary>
		/// User's display name.
		/// </summary>
		[Required]
		public string DisplayName { get; set; }

		/// <summary>
		/// User's email address.
		/// </summary>
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		/// <summary>
		/// User's phone number.
		/// </summary>
		[Required]
		[Phone]
		public string PhoneNumber { get; set; }

		/// <summary>
		/// User's password with specific complexity requirements.
		/// </summary>
		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+]).*$",
			ErrorMessage = "Password must contain 1 Uppercase, 1 Lowercase, 1 Digit, 1 Special Character")]
		public string Password { get; set; }
	}
}
