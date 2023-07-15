using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Migrations.Migrations
{
    [FluentMigrator.Migration(202307141108)]
    public class _202307141108_AddedUnits : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Units")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("ResidenceType").AsInt32().NotNullable()
                .WithColumn("Name").AsString(225).NotNullable()
                .WithColumn("BlockId").AsInt32().NotNullable()
                .ForeignKey("FK_Units_Blocks", "Blocks", "Id");
        }
        public override void Down()
        {
            Delete.Table("Units");
        }

    }
}
