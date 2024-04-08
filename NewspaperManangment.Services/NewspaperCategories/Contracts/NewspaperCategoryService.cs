using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.NewspaperCategories.Contracts
{
    public interface NewspaperCategoryService:Service
    {
        Task Add(AddNewspaperCategoryDto dto);
        Task Delete(int id);
        Task<List<GetNewspaperCategoryDto>?> GetCategories(int newspaperId);
        Task<GetNewspaperCategoryWithHighestNewsCountDto?> GetHighestNewsCount();
    }
}
