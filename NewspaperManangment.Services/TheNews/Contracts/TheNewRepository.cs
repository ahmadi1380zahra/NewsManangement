using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNews.Contracts
{
    public interface TheNewRepository:Repository
    {
        void Add(TheNew theNew);
        void Delete(TheNew theNew);
        Task<TheNew?> Find(int id);
        Task<List<GetAllTheNewDto>?> GetAll(GetTheNewFilterDto dto);
        Task<List<GetTheNewDto>?> GetMostViewd();
        Task<GetTheNewDto?> GetToIncreaseView(int id);
        Task IncreaseView(int id);
        Task<bool> IsExist(int id);
        Task<int> TotalNewsRateInOneCategoryNewspaper(int newsPaperCategoryId);
        Task<int> TotalNewsRateInOneCategoryNewspaperExceptItSelf(int newsPaperCategoryId, int id);
        void Update(TheNew theNew);
    }
}
