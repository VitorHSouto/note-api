using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Entities
{
    public abstract class IEntityBase
    {
        public IEntityBase()
        {
            Id = new Guid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Active = true;
        }

        [Column("id")]
        public Guid Id { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }

        [Column("active")]
        public bool Active { get; set; }

    }
}
