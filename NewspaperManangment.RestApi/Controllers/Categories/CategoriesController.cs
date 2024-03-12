using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;
        public CategoriesController(CategoryService Service)
        {
            _service = Service;
        }
        [HttpPost]
        public async Task Add([FromBody] AddCategoryDto dto)
        {
           await _service.Add(dto);
        }
        [HttpPut("{id}")]
        public async Task Update(int id,[FromBody]UpdateCategoryDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }
        [HttpGet]
        public async Task<List<GetCategoryDto>?> GetAll([FromQuery]GetCategoryFilterDto? dto)
        {
            return await _service.GetAll(dto);
        }
    }
}
