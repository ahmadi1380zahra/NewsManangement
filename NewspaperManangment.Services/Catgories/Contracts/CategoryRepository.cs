using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.Catgories.Contracts
{
    public interface CategoryRepository
    {
        void Add(Category category);
        void Delete(Category category);
        Task<Category?> Find(int id);
        Task<List<GetCategoryDto>?> GetAll(GetCategoryFilterDto? dto);
        Task<bool> IsExist(int id);
        void Update(Category category);
    }
}
