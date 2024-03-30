using NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.NewspaperCategories.Contracts
{
    public interface NewspaperCategoryService
    {
        Task Add(AddNewspaperCategoryDto dto);
        Task Delete(int id);
        Task<GetNewspaperCategoryDto?> GetHighestNewsCount();
    }
}
