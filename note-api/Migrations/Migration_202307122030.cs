using FluentMigrator;

namespace note_api.Migrations
{
    [Migration(202307122030)]
    public class Migration_202307122030 : FluentMigrator.Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            Rename.Column("text")
                .OnTable("note")
                .To("content");

            Create.Table("user")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("createdat").AsDateTime().NotNullable()
                .WithColumn("updatedat").AsDateTime().NotNullable()
                .WithColumn("active").AsBoolean().NotNullable()
                .WithColumn("name").AsString(100)
                .WithColumn("email").AsString(100)
                .WithColumn("password").AsString(100);

            Create.Table("user_note")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("createdat").AsDateTime().NotNullable()
                .WithColumn("updatedat").AsDateTime().NotNullable()
                .WithColumn("active").AsBoolean().NotNullable()
                .WithColumn("noteid").AsGuid().NotNullable().ForeignKey("note", "id")
                .WithColumn("userid").AsGuid().ForeignKey("user", "id");
        }
    }
}
