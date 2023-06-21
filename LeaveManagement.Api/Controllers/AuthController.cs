using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Login user, hardcoded user for testing.
    /// </summary>
    /// <returns>A JWT token</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Auth/login
    ///     {
    ///        "email": "vedran@gmail.com",
    ///        "password": "123456"
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login(AuthRequest authRequest)
    {
        var authResponse = await _authService.Login(authRequest);

        if (authResponse == null)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Not found",
                Status = 404,
                Detail = "User not found."
            });
        }

        return Ok(authResponse);
    }
}
