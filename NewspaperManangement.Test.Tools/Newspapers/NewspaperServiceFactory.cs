using Moq;
using NewspaperManangment.Contracts.Interfaces;
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
        public static NewspaperService Create(EFDataContext context,DateTime? fakeDate=null)
        {
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_=>_.UtcNow()).Returns(fakeDate?? new DateTime(2024,3,16));
            return new NewspaperAppService(
                new EFNewspaperRepository(context)
                ,new EFUnitOfWork(context)
                ,new EFCategoryRepository(context)
                ,new EFNewspaperCategoryRepository(context)
                ,dateTimeServiceMock.Object);
        }
    }
}
