using FluentMigrator;

namespace InitDatabase
{
    [Migration(20190820200000)]
    public class AddSuggestTable : Migration
    {
        public override void Down()
        {
            Delete.Table("suggest");
        }

        public override void Up()
        {            
            Create.Table("suggest")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("text").AsString(50).NotNullable().Unique();
        }
    }
}
