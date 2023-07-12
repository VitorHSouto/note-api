using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Entities
{
    public class NoteEntity : IEntityBase
    {
        [Column("text")]
        public string? Text { get; set; }
    }
}
