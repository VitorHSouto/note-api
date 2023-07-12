using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Entities
{
    public abstract class IEntityBase
    {
        public IEntityBase()
        {
            Id = new Guid();
            CreatedAt = DateTime.Now.ToUniversalTime();
            Active = true;
        }

        [Column("id")]
        public Guid Id { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("active")]
        public bool Active { get; set; }

    }
}
