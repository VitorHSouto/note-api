using note_api.ApplicationBase;
using note_api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.DTOs.Core
{
    public class NoteDTO : IEntityBaseDTO
    {
        public NoteDTO(IEntityBase entity) : base(entity)
        {
        }

        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
