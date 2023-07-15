using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondComplexManagement.Migrations.Migrations
{
    [Migration(202307151922)]
    public class _202307151922_AddedUsageTypes : Migration
    {
        public override void Up()
        {
            Create.Table("UsageTypes")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(225).NotNullable();
        }
        public override void Down()
        {
            Delete.Table("UsageTypes");
        }

    }
}
