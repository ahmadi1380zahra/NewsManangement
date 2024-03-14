using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Persistance.EF.NewspaperCategories;
using NewspaperManangment.Persistance.EF.Newspapers;
using NewspaperManangment.Services.NewspaperCategories;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.NewspaperCategories
{
    public static class NewspaperCategoryServiceFactory
    {
        public static NewspaperCategoryService Create(EFDataContext context)
        {
            return new NewspaperCategoryAppService(new EFNewspaperCategoryRepository(context)
                                 ,new EFUnitOfWork(context)
                                 ,new EFCategoryRepository(context)
                                 ,new EFNewspaperRepository(context));
        }
    }
}
