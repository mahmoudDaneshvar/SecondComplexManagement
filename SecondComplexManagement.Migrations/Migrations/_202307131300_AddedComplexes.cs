using FluentMigrator;

namespace SecondComplexManagement.Migrations.Migrations
{
    [Migration(202307131300)]
    public class _202307131300_AddedComplexes : Migration
    {
        public override void Up()
        {
            Create.Table("Complexes")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(225).NotNullable()
                .WithColumn("UnitCount").AsInt32().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Complexes");
        }

    }
}
