using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Comments.Contracts.Dtos;

namespace NewspaperManangment.Services.Comments.Contracts;

public interface CommentRepository:Repository
{
    public void Add(Comment comment);
    Task<List<GetAllCommentDto>?> GetAll();
    Task<List<GetReplyCommentsDto>> GetAllReplies(int commentId);
}