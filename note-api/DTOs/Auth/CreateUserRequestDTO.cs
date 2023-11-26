namespace note_api.DTOs.Auth
{
    public class CreateUserRequestDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
