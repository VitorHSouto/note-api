using note_api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Domain.Entities
{
    public class UserEntity : IEntityBase
    {
        [Column("name")]
        public string? Name { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }

        [Column("token")]
        public string? Token { get; set; }

        [Column("profile")]
        public RoleEnum Profile { get; set; }
    }
}
