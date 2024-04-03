using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.TheNewTags.Contracts;
using NewspaperManangment.Services.TheNewTags.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.TheNewTags
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheNewTagsController : ControllerBase
    {
        private readonly TheNewTagService _service;
        public TheNewTagsController(TheNewTagService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody] AddTheNewTagDto dto)
        {
            await _service.Add(dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await _service.Delete(id);
        }
        [HttpGet("{theNewId}")]
        public async Task<List<GetTheNewTagDto>?> GetTags([FromRoute]int theNewId)
        {
            return await _service.GetTags(theNewId);
        }
    }
}
