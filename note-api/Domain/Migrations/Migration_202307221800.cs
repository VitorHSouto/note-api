using FluentMigrator;

namespace note_api.Domain.Migrations
{
    [Migration(202307221800)]
    public class Migration_202307221800 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Alter.Table("note")
                .AddColumn("title")
                .AsString(128)
                .Nullable();

            Alter.Column("content")
                .OnTable("note")
                .AsString(4096)
                .Nullable();
        }
    }
}
