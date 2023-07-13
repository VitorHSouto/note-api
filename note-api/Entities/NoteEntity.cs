using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Entities
{
    public class NoteEntity : IEntityBase
    {
        [Column("content")]
        public string? Content { get; set; }
    }
}
