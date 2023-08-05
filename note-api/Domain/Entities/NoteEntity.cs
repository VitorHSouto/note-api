using note_api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Domain.Entities
{
    public class NoteEntity : IEntityBase
    {
        [Column("title")]
        public string? Title { get; set; }
        [Column("content")]
        public string? Content { get; set; }
    }
}
