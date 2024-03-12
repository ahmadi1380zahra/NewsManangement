using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Catgories.Contracts
{
    public interface CategoryService
    {
        Task Add(AddCategoryDto dto);
        Task Delete(int id);
        Task<List<GetCategoryDto>?> GetAll(GetCategoryFilterDto? dto);
        Task Update(int id, UpdateCategoryDto dto);
    }
}
