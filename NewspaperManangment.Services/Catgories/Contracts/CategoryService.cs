using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Catgories.Contracts
{
    public interface CategoryService:Service
    {
        Task Add(AddCategoryDto dto);
        Task Delete(int id);
        Task<List<GetCategoryDto>?> GetAll(GetCategoryFilterDto? dto);
        Task<List<GetCategoryWithTagsDto>?> GetWithTags(GetCategoryFilterDto? dto);
        Task Update(int id, UpdateCategoryDto dto);
    }
}
