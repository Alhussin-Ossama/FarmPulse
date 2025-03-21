namespace FarmPulse.API.Errors
{
	public class ApiExceptionResponse : ApiRespons
	{
		public string? Details { get; set; }

		public ApiExceptionResponse(int StatusCode, string? Message = null, string? details = null) : base(StatusCode, Message)
		{
			Details = details;
		}
	}
}
