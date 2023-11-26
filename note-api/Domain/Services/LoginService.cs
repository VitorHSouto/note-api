using note_api.DTOs.Auth;

namespace note_api.Domain.Services
{
    public class LoginService
    {
        private UserService _userService;
        public LoginService(UserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDTO> Login(LoginRequestDTO req)
        {
            var user = await _userService.GetByLogin(req.Email, req.Password);
            if (user == null)
                throw new Exception("Usuário não encontrado");

            var token = TokenService.GenerateToken(user.Email, user.Profile);
            user.Token = token;

            return user;
        }
    }
}
