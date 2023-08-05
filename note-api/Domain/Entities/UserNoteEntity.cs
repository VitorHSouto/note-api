using note_api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace note_api.Domain.Entities
{
    public class UserNoteEntity : IEntityBase
    {
        [Column("userid")]
        public Guid UserId { get; set; }

        [Column("noteid")]
        public Guid NoteId { get; set; }
    }
}
