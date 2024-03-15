using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Migrations
{
    [Migration(202403151355)]
    public class _202403151355_AddTheNewTBl : Migration
    {
        public override void Up()
        {
            Create.Table("TheNews")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Description").AsString(200).NotNullable()
                .WithColumn("Rate").AsInt32().NotNullable()
                .WithColumn("View").AsInt32().NotNullable()
                .WithColumn("NewspaperCategoryId").AsInt32().NotNullable()
                  .ForeignKey("FK_TheNews_NewspaperCategories", "NewspaperCategories", "Id")
                  .OnDelete(Rule.Cascade)
                .WithColumn("AuthorId").AsInt32().NotNullable()
                  .ForeignKey("FK_TheNews_Authors", "Authors", "Id");
        }

        public override void Down()
        {
            Delete.Table("TheNews");
        }

     
    }
}
