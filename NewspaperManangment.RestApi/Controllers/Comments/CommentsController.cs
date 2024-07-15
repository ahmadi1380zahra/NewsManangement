using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.Comments.Contracts;
using NewspaperManangment.Services.Comments.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.Comments;
[Route("api/[controller]")]
public class CommentsController:ControllerBase
{
    private readonly CommentService _service;

    public CommentsController(CommentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task Add([FromBody]AddCommentDto dto)
    {
        await _service.Add(dto);
    }

    [HttpGet]
    public async Task<List<GetAllCommentDto>?> Get()
    {
      return  await _service.GetAll();
    }
    [HttpGet("get-replies/{id}")]
    public async Task<List<GetReplyCommentsDto>> GetReply(int id)
    {
        return  await _service.GetAllReplies(id);
    }
}