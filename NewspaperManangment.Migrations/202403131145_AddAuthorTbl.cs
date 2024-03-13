using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Migrations
{
    [Migration(202403131145)]
    public class _202403131145_AddAuthorTbl : Migration
    {
        public override void Up()
        {
            Create.Table("Authors")

                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FullName").AsString(50).NotNullable();
        }
        public override void Down()
        {
            Delete.Table("Authors");
        }


    }
}
