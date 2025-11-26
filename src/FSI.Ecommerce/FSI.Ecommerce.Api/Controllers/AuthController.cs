using FSI.Ecommerce.Application.Dtos.Auth;
using FSI.Ecommerce.Application.Dtos.Common;
using FSI.Ecommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSI.Ecommerce.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public sealed class AuthController : ControllerBase
    {
        private readonly IAuthAppService _authAppService;

        public AuthController(IAuthAppService authAppService)
        {
            _authAppService = authAppService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultDto<LoginResponseDto>>> Login(
            [FromBody] LoginRequestDto request,
            CancellationToken ct)
        {
            var result = await _authAppService.LoginAsync(request, ct);

            if (!result.Success || result.Data is null)
                return Unauthorized(new { error = result.Error });

            return Ok(result);
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<object> Me()
        {
            return Ok(new
            {
                userId = User.Identity?.Name ?? User.FindFirst("sub")?.Value,
                email = User.FindFirst("email")?.Value
            });
        }
    }
}
