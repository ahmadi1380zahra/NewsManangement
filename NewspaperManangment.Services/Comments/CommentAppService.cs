using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Comments.Contracts;
using NewspaperManangment.Services.Comments.Contracts.Dtos;

namespace NewspaperManangment.Services.Comments;

public class CommentAppService:CommentService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly CommentRepository _repository;

    public CommentAppService(UnitOfWork unitOfWork,CommentRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task Add(AddCommentDto dto)
    {
        var comment = new Comment
        {
            TheNewId = dto.TheNewId,
            comment = dto.comment,
            ReplyId = dto.ReplyId
        };
        _repository.Add(comment);
        await _unitOfWork.Complete();
    }

    public async Task<List<GetAllCommentDto>?> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<List<GetReplyCommentsDto>> GetAllReplies(int commentId)
    {
        return await _repository.GetAllReplies(commentId);
    }
}