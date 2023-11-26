using FluentMigrator;
using note_api.Domain.Services;

namespace note_api.Domain.Migrations
{
    [Migration(202308051930)]
    public class Migration_202308051930 : FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Alter.Table("user")
                .AddColumn("token").AsString(1024).Nullable()
                .AddColumn("profile").AsString(50).Nullable();

            var id = Guid.NewGuid();
            var now = DateTime.UtcNow;
            var passwordHash = TokenService.HashPassword("dev@mail.com");

            Execute.Sql(@$"INSERT INTO public.user 
                    (id, active, updatedat, createdat, name, email, password, profile)
                    VALUES ('{id}', 'true', '{now}', '{now}', 'Admin', 'dev@mail.com', '{passwordHash}', 'Admin');");
        }
    }
}
