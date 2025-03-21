using FarmPulse.API.DTOs;
using FarmPulse.API.Errors;
using FarmPulse.Core.Identity;
using FarmPulse.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FarmPulse.API.Controllers
{
	/// <summary>
	/// Handles user account-related operations such as registration, login, and fetching the current user.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		/// <summary>
		/// Registers a new user with the provided information.
		/// </summary>
		/// <param name="model">The registration details including email, password, display name, and phone number.</param>
		/// <returns>User details along with a JWT token upon successful registration.</returns>
		[HttpPost("Register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{

			if (CheckEmailExists(model.Email).Result.Value)
			{
				return BadRequest(new ApiRespons(400, "Email Is Already in Use"));
			}
			var User = new AppUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.Email.Split('@')[0],
				PhoneNumber = model.PhoneNumber
			};
			var Result = await _userManager.CreateAsync(User, model.Password);
			if (!Result.Succeeded) return BadRequest(new ApiRespons(400));

			var ReturnedUser = new UserDto()
			{
				DisplayName = User.DisplayName,
				Email = User.Email,
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			};

			return Ok(ReturnedUser);
		}


		/// <summary>
		/// Authenticates a user and returns user details with a JWT token.
		/// </summary>
		/// <param name="model">The login credentials (email and password).</param>
		/// <returns>User details with JWT token if credentials are valid.</returns>
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var User = await _userManager.FindByEmailAsync(model.Email);
			if (User == null) return Unauthorized(new ApiRespons(401));

			var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
			if (!Result.Succeeded) return Unauthorized(new ApiRespons(401));
			var ReturnedUser = new UserDto()
			{
				DisplayName = User.DisplayName,
				Email = User.Email,
				Token = await _tokenService.CreateTokenAsync(User, _userManager)
			};
			return Ok(ReturnedUser);
		}

		/// <summary>
		/// Retrieves the currently authenticated user's information.
		/// </summary>
		/// <returns>User details with a fresh JWT token.</returns>
		[Authorize]
		[HttpGet("GetCurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var Email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(Email);
			var ReturnedObject = new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			};
			return Ok(ReturnedObject);
		}

		/// <summary>
		/// Checks if an email is already registered.
		/// </summary>
		/// <param name="Email">The email to check.</param>
		/// <returns>True if the email exists, false otherwise.</returns>
		[HttpGet("emailExists")]
		public async Task<ActionResult<bool>> CheckEmailExists(string Email)
		{
			return await _userManager.FindByEmailAsync(Email) is not null;
		}
	}
}
