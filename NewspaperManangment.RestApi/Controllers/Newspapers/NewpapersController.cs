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
        public async Task Update(int id,[FromBody] UpdateNewsPaperDto dto)
        {
            await _service.Update(id,dto);
        }
    }
}
