using FluentMigrator;
using FluentMigrator.Postgres;

namespace NewspaperManangment.Migrations;
[Migration(202407151321)]
public class _202407151321_AddCommentsTBl:Migration {
    public override void Up()
    {
        Create.Table("Comments")
            .WithColumn("Id").AsInt32()
            .PrimaryKey().Identity()
            .WithColumn("comment")
            .AsString().NotNullable()
            .WithColumn("TheNewId")
            .AsInt32().ForeignKey("FK_Comments_TheNews","TheNews","Id")
            .NotNullable()
            .WithColumn("ReplyId")
            .AsInt32()
            .ForeignKey("FK_Comments_Comments","Comments","Id")
            .Nullable();
        

    }

    public override void Down()
    {
        Delete.Table("Comments");

    }
}