using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Migrations
{
    [Migration(202403122031)]
    public class _202403122031_AddTagTBL : Migration
    {
        public override void Up()
        {
            Create.Table("Tags")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("CategoryId").AsInt32().NotNullable()
                   .ForeignKey("FK_Tags_Categories", "Categories", "Id");
        }
        public override void Down()
        {
            Delete.Table("Tags");
        }

     
    }
}
