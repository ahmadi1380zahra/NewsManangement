using NewspaperManangment.Services.Comments.Contracts.Dtos;

namespace NewspaperManangement.Test.Tools.Comments;

public class AddCommentDtoBuilder
{
    private readonly AddCommentDto _dto;

    public AddCommentDtoBuilder()
    {
        _dto = new AddCommentDto
        {
            comment = "dummy-description"
        };
    }

    public AddCommentDtoBuilder WithReplyId(int id)
    {
        _dto.ReplyId = id;
        return this;
    }
    public AddCommentDtoBuilder WithTheNewId(int id)
    {
        _dto.TheNewId = id;
        return this;
    }
    public AddCommentDtoBuilder WithComment(string comment)
    {
        _dto.comment = comment;
        return this;
    }

    public AddCommentDto Build()
    {
        return _dto;
    }
}