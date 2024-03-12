using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.Tags.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.Tags
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TagService _service;
        public TagsController(TagService Service)
        {
            _service = Service;
        }
        [HttpPost]
        public async Task Add([FromBody] AddTagDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPut("{id}")]
        public async Task Update(int id, [FromBody] UpdateTagDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetTagDto>?> GetAll([FromQuery] GetTagFilterDto? dto)
        {
            return await _service.GetAll(dto);
        }
    }
}
