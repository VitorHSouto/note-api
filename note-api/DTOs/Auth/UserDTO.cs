using note_api.ApplicationBase;
using note_api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.DTOs.Auth
{
    public class UserDTO : IEntityBaseDTO
    {
        public UserDTO(IEntityBase entity) : base(entity)
        {
        }

        public string? Token { get; set; }
        public RoleEnum Profile { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
