using FluentMigrator;

namespace SecondComplexManagement.Migrations.Migrations
{
    [Migration(202307131435)]
    public class _202307131435_AddedBlocks : Migration
    {
        public override void Up()
        {
            Create.Table("Blocks")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("ComplexId").AsInt32().NotNullable()
                   .ForeignKey("FK_Blocks_Complexes", "Complexes", "Id")
                .WithColumn("UnitCount").AsInt32().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Blocks");
        }

    }
}
