using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Entities
{
    public class UserNoteEntity : IEntityBase
    {
        [Column("userid")]
        public Guid UserId { get; set; }

        [Column("noteid")]
        public Guid NoteId { get; set; }
    }
}
