using Microsoft.EntityFrameworkCore;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Persistance.EF.NewspaperCategories
{
    public class EFNewspaperCategoryRepository : NewspaperCategoryRepository
    {
        private readonly DbSet<NewspaperCategory> _newspaperCategories;
        public EFNewspaperCategoryRepository(EFDataContext context)
        {
            _newspaperCategories = context.Set<NewspaperCategory>();
        }

        public void Add(NewspaperCategory newspaperCategory)
        {
            _newspaperCategories.Add(newspaperCategory);
        }

        public async Task<bool> IsReduplicateCategoryIdForThisNewspaper(int newspaperId, int categoryId)
        {
            return await _newspaperCategories.AnyAsync(_ => _.NewspaperId == newspaperId
            && _.CategoryId == categoryId);
        }
    }
}
