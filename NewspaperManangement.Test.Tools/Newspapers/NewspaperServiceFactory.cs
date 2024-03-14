using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Persistance.EF.NewspaperCategories;
using NewspaperManangment.Persistance.EF.Newspapers;
using NewspaperManangment.Services.Newspapers;
using NewspaperManangment.Services.Newspapers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Newspapers
{
    public static class NewspaperServiceFactory
    {
        public static NewspaperService Create(EFDataContext context)
        {
            return new NewspaperAppService(
                new EFNewspaperRepository(context)
                ,new EFUnitOfWork(context)
                ,new EFCategoryRepository(context)
                ,new EFNewspaperCategoryRepository(context));
        }
    }
}
