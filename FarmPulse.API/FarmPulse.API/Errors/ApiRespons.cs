
namespace FarmPulse.API.Errors
{
	public class ApiRespons
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }

		public ApiRespons(int statusCode, string? message = null)
		{
			StatusCode = statusCode;
			Message = message??GetDefaultMessageForStatusCode(statusCode);
		}

		private string? GetDefaultMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "BadRequest",
				401 => "You Are Not Authorized",
				404 => "Resource Not Found",
				500 => "Internal Server Error",
				_ => null
			};
		}
	}
}
