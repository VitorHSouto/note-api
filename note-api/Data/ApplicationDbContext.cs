using Microsoft.EntityFrameworkCore;
using note_api.Entities;

namespace note_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<NoteEntity> note { get; set; }
        public DbSet<UserEntity> user { get; set; }
        public DbSet<UserNoteEntity> usernote { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }
    }
}
