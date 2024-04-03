using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.NewspaperCategories;
using NewspaperManangment.Persistance.EF.Tags;
using NewspaperManangment.Persistance.EF.TheNews;
using NewspaperManangment.Persistance.EF.TheNewTags;
using NewspaperManangment.Services.TheNewTags;
using NewspaperManangment.Services.TheNewTags.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.TheNewTags
{
    public static class TheNewTagServiceFactory
    {
        public static TheNewTagService Create(EFDataContext context)
        {
           return new TheNewTagAppService(new EFTheNewTagRepository(context)
                , new EFUnitOfWork(context)
                ,new EFTagRepository(context)
                ,new EFTheNewRepository(context)
                ,new EFNewspaperCategoryRepository(context));
        }
    }
}
