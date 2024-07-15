using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Comments.Contracts;
using NewspaperManangment.Services.Comments.Contracts.Dtos;

namespace NewspaperManangment.Persistance.EF.Comments;

public class EFCommentRepository:CommentRepository
{
    private readonly DbSet<Comment> _comments;

    public EFCommentRepository(EFDataContext dataContext)
    {
        _comments = dataContext.Set<Comment>();
    }

    public void Add(Comment comment)
    {
        _comments.Add(comment);
    }

    public async Task<List<GetAllCommentDto>?> GetAll()
    {
        return await _comments.Where(_ => _.ReplyId == null).Select(
            c => new GetAllCommentDto
            {
                Id = c.Id,
                Comment = c.comment,
                Replies = c.theNew_Comments.Select(r => new GetReplyCommentsDto
                {
                    Id = r.Id,
                    Descripton = r.comment,
                    replyId = r.ReplyId,
                }).ToList()
            }
        ).ToListAsync();
    }

    public async Task<List<GetReplyCommentsDto>> GetAllReplies(int commentId)
    {
        return await _comments.Where(_ => _.ReplyId == commentId)
            .Select(c => new GetReplyCommentsDto
            {
                Id = c.Id,
                Descripton = c.comment,
                replyId = c.ReplyId
            }).ToListAsync();
    }
}