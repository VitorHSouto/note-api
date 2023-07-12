using FluentMigrator;
using Microsoft.EntityFrameworkCore.Storage;

namespace note_api.Migrations
{
    [Migration(202307122000)]
    public class Migration_202307122000 : FluentMigrator.Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create.Table("note")
                .WithColumn("id").AsGuid().PrimaryKey().NotNullable()
                .WithColumn("createdat").AsDateTime().NotNullable()
                .WithColumn("updatedat").AsDateTime().NotNullable()
                .WithColumn("active").AsBoolean().NotNullable()
                .WithColumn("text").AsString(2048).NotNullable();
        }
    }
}
