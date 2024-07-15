using System.ComponentModel.DataAnnotations;

namespace NewspaperManangment.Services.Comments.Contracts.Dtos;

public class AddCommentDto
{
    [Required]
    public int TheNewId  { get; set; }
    [Required]
    public string comment {get;set;}
    public int? ReplyId { get; set; }
}