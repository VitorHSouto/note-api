using FluentMigrator;

namespace note_api.Migrations
{
    [Migration(202307221800)]
    public class Migration_202307221800 : FluentMigrator.Migration
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
