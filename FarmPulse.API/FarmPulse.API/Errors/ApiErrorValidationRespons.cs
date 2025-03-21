namespace FarmPulse.API.Errors
{
	public class ApiErrorValidationRespons:ApiRespons
	{
        public IEnumerable<string> Errors { get; set; }
        public ApiErrorValidationRespons() :base(400)
		{
			Errors = new List<string>();
		}
	}
}
