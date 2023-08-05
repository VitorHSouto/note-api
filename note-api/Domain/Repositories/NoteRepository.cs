using note_api.Data;
using note_api.Domain.Entities;

namespace note_api.Domain.Repositories
{
    public class NoteRepository : RepositoryBase<NoteEntity>
    {
        public NoteRepository(ApplicationDbContext dbContext) : base(dbContext, "note")
        {

        }
    }
}
