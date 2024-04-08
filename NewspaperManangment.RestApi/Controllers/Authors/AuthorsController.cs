using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.Authors
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _service;
        public AuthorsController(AuthorService service)
        {
            _service=service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddAuthorDto dto)
        {
           await _service.Add(dto);
        }
        [HttpPut("{id}")]
        public async Task Update([FromRoute]int id,[FromBody] UpdateAuthorDto dto)
        {
            await _service.Update(id,dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetAuthorsDto>?> GetAll([FromQuery]GetAuthorsFilterDto? dto)
        {
           return await _service.GetAll(dto);
        }
        [HttpGet("Get")]
        public async Task<GetAuthorsDto>? Get([FromQuery] int id)
        {
            return await _service.Get(id);
        }
        [HttpGet("MostViewed")]
        public async Task<List<GetAuthorsDto>?> MostViewed()
        {
            return await _service.GetMostViewed();
        }
        [HttpGet("GetHighestNewsCount")]
        public async Task<List<GetAuthorsDto>?> GetHighestNewsCount()
        {
            return await _service.GetHighestNewsCount();
        }

    }
}
