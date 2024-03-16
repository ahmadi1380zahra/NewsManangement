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
        [HttpGet]
        public async Task<GetTheNewDto?> GetToIncreaseViews([FromQuery]int id)
        {
          return  await _service.GetToIncreaseView(id);
        }
    }
}
