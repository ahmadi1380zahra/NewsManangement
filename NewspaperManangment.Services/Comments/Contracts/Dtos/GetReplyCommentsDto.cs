namespace NewspaperManangment.Services.Comments.Contracts.Dtos;

public class GetReplyCommentsDto
{
    public int Id {
        get;
        set;
    }

    public string Descripton { get; set; }
    public int? replyId { get; set; }
}