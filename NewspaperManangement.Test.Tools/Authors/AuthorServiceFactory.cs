using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Authors;
using NewspaperManangment.Services.Authors;
using NewspaperManangment.Services.Authors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Test.Tools.Authors
{
    public static class AuthorServiceFactory
    {
        public static AuthorService Create(EFDataContext context)
        {
            return new AuthorAppService
                (new EFAuthorRepository(context), new EFUnitOfWork(context));
        }
    }
}
