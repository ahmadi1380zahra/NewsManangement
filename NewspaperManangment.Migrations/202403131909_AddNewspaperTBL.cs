using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Migrations
{
    [Migration(202403131909)]
    public class _202403131909_AddNewspaperTBL : Migration
    {
        public override void Up()
        {
            Create.Table("Newspapers")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("PublishDate").AsDate().Nullable();
        }
        public override void Down()
        {
            Delete.Table("Newspapers");
        }


    }
}
