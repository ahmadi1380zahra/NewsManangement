using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Services.Catgories;
using NewspaperManangment.Services.Catgories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Categories
{
    public static class CategoryServiceFactory
    {
        public static CategoryService Create(EFDataContext context)
        {
            return new  CategoryAppService
              (new EFCategoryRepository(context),
              new EFUnitOfWork(context));
        }
    }
}
