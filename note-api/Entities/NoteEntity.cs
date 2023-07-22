using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Entities
{
    public class NoteEntity : IEntityBase
    {
        [Column("title")]
        public string? Title { get; set; }
        [Column("content")]
        public string? Content { get; set; }
    }
}
