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
        void Delete(NewspaperCategory newspaperCategory);
        void DeleteCategoryForThisNewspaper(int id);
        Task<NewspaperCategory?> Find(int id);
        Task<bool> IsExist(int id);
        Task<bool> IsExistCategoryForThisNewspaper(int id);
        Task<bool> IsReduplicateCategoryIdForThisNewspaper(int newspaperId, int categoryId);
    }
}
