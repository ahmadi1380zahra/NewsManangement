using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Persistance.EF.Tags;
using NewspaperManangment.Services.Tags;
using NewspaperManangment.Services.Tags.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Tags
{
    public static class TagServiceFactory
    {
        public static TagService Create(EFDataContext context)
        {
            return new TagAppService(new EFTagRepository(context)
                                    , new EFUnitOfWork(context)
                                    ,new EFCategoryRepository(context));
        }
    }
}
