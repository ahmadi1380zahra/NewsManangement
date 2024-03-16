using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.Newspapers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewpapersController : ControllerBase
    {
        private readonly NewspaperService _service;
        public NewpapersController(NewspaperService service)
        {
                _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody] AddNewsPaperDto dto)
        {
          await  _service.Add(dto);
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute]int id,[FromBody] UpdateNewsPaperDto dto)
        {
            await _service.Update(id,dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetNewspaperDto>?> GetAll([FromQuery]GetNewspaperFilterDto? dto)
        {
            return await _service.GetAll(dto);
        }
        [HttpPatch("Publish/{id}")]
        public async Task Publish([FromRoute] int id)
        {
            await _service.Publish(id);
        }
    }
}
