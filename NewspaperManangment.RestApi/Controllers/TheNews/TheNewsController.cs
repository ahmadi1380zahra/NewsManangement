using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.TheNews
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheNewsController : ControllerBase
    {
        private readonly TheNewService _service;
        public TheNewsController(TheNewService service)
        {
            _service = service;     
        }
        [HttpPost]
        public async Task Add([FromBody] AddTheNewDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute]int id,[FromBody] UpdateTheNewDto dto)
        {
            await _service.Update(id,dto);
        }
        [HttpGet("ToIncreaseViews")]
        public async Task<GetTheNewDto?> GetToIncreaseViews([FromQuery]int id)
        {
          return  await _service.GetToIncreaseView(id);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
             await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetAllTheNewDto>?> GetAll([FromQuery]GetTheNewFilterDto dto)
        {
            return await _service.GetAll(dto);
        }
        [HttpGet("MostView")]
        public async Task<List<GetTheNewDto>?> GetMostView()
        {
            return await _service.GetMostViewd();
        }
    }
}
