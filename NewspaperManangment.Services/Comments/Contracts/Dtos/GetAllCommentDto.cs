namespace NewspaperManangment.Services.Comments.Contracts.Dtos;

public class GetAllCommentDto
{
   public int Id {
      get;
      set;
   }

   public string Comment { get; set; }
   public List<GetReplyCommentsDto> Replies { get; set; }
}