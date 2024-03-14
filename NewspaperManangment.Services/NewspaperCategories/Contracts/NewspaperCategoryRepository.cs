using NewspaperManangment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangment.Services.NewspaperCategories.Contracts
{
    public interface NewspaperCategoryRepository
    {
        void Add(NewspaperCategory newspaperCategory);
        Task<bool> IsReduplicateCategoryIdForThisNewspaper(int newspaperId, int categoryId);
    }
}
