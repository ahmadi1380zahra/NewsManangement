using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos;

namespace NewspaperManangment.RestApi.Controllers.NewspaperCategories
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewspaperCategoriesController : ControllerBase
    {
        private readonly NewspaperCategoryService _service;
        public NewspaperCategoriesController(NewspaperCategoryService service)
        {
                _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddNewspaperCategoryDto dto)
        {
          await  _service.Add(dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await _service.Delete(id);
        }
      
    }
}
