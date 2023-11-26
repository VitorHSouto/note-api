using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using note_api.Domain.Repositories;
using note_api.Domain.Services;
using note_api.DTOs.Auth;

namespace note_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("")]
        public async Task<UserDTO> Login(LoginRequestDTO req)
        {
            return await _loginService.Login(req);
        }
    }
}
