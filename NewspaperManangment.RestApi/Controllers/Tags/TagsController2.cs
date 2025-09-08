using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Persistance.EF.Mediate.Tag4;
using NewspaperManangment.Services.Application.Tag3;
using NewspaperManangment.Services.Application.Tags;
using NewspaperManangment.Services.Application.Tags2;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.Tags.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.Tags
{
    [Route("api/[controller]")]
    [ApiController]
    public class Tags2Controller : ControllerBase
    {
        private readonly TagService _service;
      
        public Tags2Controller(TagService Service
            )
        {
            _service = Service;
          
        }

        [HttpPost]
        public async Task<int> Add([FromBody] AddTagCommand command,[FromServices]
            ICommandHandler<AddTagCommand,int> _handler
            )
        {
          return  await _handler.Handle(command,null);
          
        }
        [HttpPost("with-tenant")]
        public async Task<AddTagCommandHandler2.MyClass> Add2([FromBody] AddTagCommand2 command,[FromServices]
            ICommandHandler<AddTagCommand2,AddTagCommandHandler2.MyClass> _handler
        )
        {
            
            return  await _handler.Handle(command,new BasicRequest
            {
                TenantId = "fep tenant iddd",
                UserId = "zara user idd"
            });
          
        }
        [HttpPost("no-response")]
        public async Task Add2([FromBody] AddTagCommand3 command,[FromServices]
            ICommandHandler<AddTagCommand3> _handler
        )
        {
            
              await _handler.Handle(command,new BasicRequest
            {
                TenantId = "fep tenant iddd",
                UserId = "zara user idd"
            });
          
        }

        [HttpGet("detail")]
        public async Task<GetTagDto2?> Get(
            [FromQuery] GetTagDtoById dto,
            [FromServices] IQueryHandler<GetTagDtoById,GetTagDto2> query
           )
        {
            return await query.Handle(dto);
        }
        [HttpGet("all")]
        public async Task<List<GetAllTag>> GetAll(
            [FromServices] ISimpleQueryHandler<List<GetAllTag>> simpleQuery
        )
        {
            return await simpleQuery.Handle();
        }
    }
}
