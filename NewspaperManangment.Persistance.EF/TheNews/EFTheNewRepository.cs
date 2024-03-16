using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNews.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.TheNews
{
    public class EFTheNewRepository : TheNewRepository
    {
        private readonly DbSet<TheNew> _theNews;
        public EFTheNewRepository(EFDataContext context)
        {
            _theNews = context.Set<TheNew>();
        }

        public void Add(TheNew theNew)
        {
         _theNews.Add(theNew);
        }

        public async Task<int> TotalNewsRateInOneCategoryNewspaper(int newsPaperCategoryId)
        {
            return await _theNews 
                .Where(_ => _.NewspaperCategoryId == newsPaperCategoryId)
               .SumAsync(_=>_.Rate);
        }
    }
}
