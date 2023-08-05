using note_api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.ApplicationBase
{
    public abstract class IEntityBaseDTO
    {
        public IEntityBaseDTO(IEntityBase entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            UpdatedAt = entity.UpdatedAt;
            Active = entity.Active;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; }
    }
}
