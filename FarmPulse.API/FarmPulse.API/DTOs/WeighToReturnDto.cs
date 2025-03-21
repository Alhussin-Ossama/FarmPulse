namespace FarmPulse.API.DTOs
{
	/// <summary>
	/// Data Transfer Object representing the weight history of a chicken.
	/// </summary>
	public class WeighToReturnDto
	{
		/// <summary>
		/// The weight of the chicken when it first entered the weighing station.
		/// </summary>
		public double EntryWeight { get; set; }

		/// <summary>
		/// The weight of the chicken when it last exited the weighing station.
		/// Default is 0 if not recorded.
		/// </summary>
		public double ExitWeight { get; set; } = 0;

		/// <summary>
		/// The timestamp when the chicken's entry weight was recorded.
		/// </summary>
		public DateTime EntryTime { get; set; } = DateTime.Now;

		/// <summary>
		/// The timestamp when the chicken's exit weight was recorded.
		/// Default is the current time if not recorded.
		/// </summary>
		public DateTime ExitTime { get; set; } = DateTime.Now;
	}
}
