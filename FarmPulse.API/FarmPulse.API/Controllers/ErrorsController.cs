using FarmPulse.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmPulse.API.Controllers
{
	[Route("errors/{Code}")]
	[ApiController]

	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorsController : ControllerBase
	{
		public ActionResult Error(int Code)
		{
			return NotFound(new ApiRespons(Code));
		}
	}
}
