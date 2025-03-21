namespace FarmPulse.API.DTOs
{
	/// <summary>
	/// Data Transfer Object representing user information returned after authentication.
	/// </summary>
	public class UserDto
	{
		/// <summary>
		/// The display name of the user.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// The email address of the user.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The JWT token generated for the authenticated user.
		/// </summary>
		public string Token { get; set; }
	}
}
