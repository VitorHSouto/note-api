using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Entities
{
    public class UserEntity : IEntityBase
    {
        [Column("name")]
        public string? name { get; set; }
                
        [Column("email")]
        public string? Email { get; set; }

        [Column("password")]
        public string? Password { get; set; }
    }
}
