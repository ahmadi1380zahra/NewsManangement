using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Authors;
using NewspaperManangment.Persistance.EF.NewspaperCategories;
using NewspaperManangment.Persistance.EF.Tags;
using NewspaperManangment.Persistance.EF.TheNews;
using NewspaperManangment.Services.TheNews;
using NewspaperManangment.Services.TheNews.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.TheNews
{
    public static class TheNewServiceFactory
    {
        public static TheNewService Create(EFDataContext context)
        {
            return new TheNewAppService(new EFTheNewRepository(context)
                , new EFUnitOfWork(context)
                ,new EFAuthorRepository(context)
                ,new EFNewspaperCategoryRepository(context)
                ,new EFTagRepository(context));
        }

    }
}
