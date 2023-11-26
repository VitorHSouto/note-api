using Microsoft.AspNetCore.Mvc;
using note_api.Domain.Services;
using note_api.DTOs.Auth;
using note_api.DTOs.Core;

namespace note_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<List<UserDTO>> ListAll()
        {
            return await _userService.ListAll();
        }

        [HttpGet("{id}")]
        public async Task<UserDTO> Get(Guid id)
        {
            return await _userService.GetById(id);
        }

        [HttpPost("")]
        public async Task<UserDTO> Save(CreateUserRequestDTO req)
        {
            return await _userService.Save(req);
        }
    }
}
