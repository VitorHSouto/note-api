namespace note_api.DTOs.Auth
{
    public class LoginRequestDTO
    {
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
