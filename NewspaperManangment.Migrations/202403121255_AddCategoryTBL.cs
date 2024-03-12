using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Migrations
{
    [Migration(202403121255)]
    public class _202403121255_AddCategoryTBL : Migration
    {
        public override void Up()
        {
            Create.Table("Categories")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Rate").AsInt32().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Categories");
        }

    
    }
}
