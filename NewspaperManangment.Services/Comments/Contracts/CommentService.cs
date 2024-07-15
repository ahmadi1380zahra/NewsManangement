using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Services.Comments.Contracts.Dtos;

namespace NewspaperManangment.Services.Comments.Contracts;

public interface CommentService:Service
{
    Task Add(AddCommentDto dto);
    Task<List<GetAllCommentDto>?> GetAll();
    Task<List<GetReplyCommentsDto>> GetAllReplies(int commentId);
}