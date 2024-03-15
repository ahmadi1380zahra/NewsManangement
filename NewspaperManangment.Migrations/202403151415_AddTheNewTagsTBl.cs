using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Migrations
{
    [Migration(202403151415)]
    public class _202403151415_AddTheNewTagsTBl : Migration
    {
        public override void Up()
        {
            Create.Table("TheNewTags")
           .WithColumn("Id").AsInt32().PrimaryKey().Identity()
           .WithColumn("TagId").AsInt32().NotNullable()
              .ForeignKey("FK_TheNewTags_Tags", "Tags", "Id")
           .WithColumn("TheNewId").AsInt32().NotNullable()
             .ForeignKey("FK_TheNewTags_TheNews", "TheNews", "Id")
             .OnDelete(Rule.Cascade);

        }

        public override void Down()
        {
            Delete.Table("TheNewTags");
        }
    }
}
