using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.TheNews.Contracts
{
    public interface TheNewRepository
    {
        void Add(TheNew theNew);
        Task<int> TotalNewsRateInOneCategoryNewspaper(int newsPaperCategoryId);
    }
}
