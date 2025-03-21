namespace FarmPulse.API.DTOs
{
	/// <summary>
	/// Data Transfer Object representing a notification sent to the user.
	/// </summary>
	public class NotificationDto
	{
		/// <summary>
		/// Unique identifier for the notification.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The message content of the notification.
		/// </summary>
		public string? Message { get; set; }

		/// <summary>
		/// The date and time when the notification was created.
		/// </summary>
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
