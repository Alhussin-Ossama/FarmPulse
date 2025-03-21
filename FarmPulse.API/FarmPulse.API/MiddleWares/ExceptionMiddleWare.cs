using FarmPulse.API.Errors;
using System.Net;
using System.Text.Json;

namespace FarmPulse.API.MiddleWares
{
	public class ExceptionMiddleWare
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleWare> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleWare(RequestDelegate Next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
		{
			_next = Next;
			_logger = logger;
			_env = env;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = 500;


				var Response = _env.IsDevelopment() ? new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()): new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				var Options = new JsonSerializerOptions()
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				};
				var JsonResponse = JsonSerializer.Serialize(Response, Options);
				await context.Response.WriteAsync(JsonResponse);
			}
		}
	}
}
