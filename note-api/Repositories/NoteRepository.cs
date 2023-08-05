using note_api.Data;
using note_api.Entities;

namespace note_api.Repositories
{
    public class NoteRepository : RepositoryBase<NoteEntity>
    {
        public NoteRepository(ApplicationDbContext dbContext) : base(dbContext, "note")
        {

        }
    }
}
