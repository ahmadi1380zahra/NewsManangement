using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Migrations
{
    [Migration(202403131919)]
    public class _202403131919_AddNewspaperCategoryTbl : Migration
    {
        public override void Up()
        {
            Create.Table("NewspaperCategories")
                 .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("NewspaperId").AsInt32().NotNullable()
                  .ForeignKey("FK_NewspaperCategories_Newspapers", "Newspapers", "Id")
                .WithColumn("CategoryId").AsInt32().NotNullable()
                  .ForeignKey("FK_NewspaperCategories_Categories", "Categories", "Id");
        }
        public override void Down()
        {
            Delete.Table("NewspaperCategories");
        }


    }
}
