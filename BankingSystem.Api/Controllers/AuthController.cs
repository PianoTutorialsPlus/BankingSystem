using BankingSystem.Application.Contracts.Identity;
using BankingSystem.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authenticationService;

        public AuthController(IAuthService authenticationService)
        {
            this.authenticationService = authenticationService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
        {
            return Ok(await authenticationService.Login(request));
        }
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await authenticationService.Register(request));
        }
    }
}
