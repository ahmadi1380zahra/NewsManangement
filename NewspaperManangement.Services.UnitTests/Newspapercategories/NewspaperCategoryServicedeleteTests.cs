using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.NewspaperCategories.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Newspapercategories
{
    public class NewspaperCategoryServicedeleteTests : BusinessUnitTest
    {
        private readonly NewspaperCategoryService _sut;
        public NewspaperCategoryServicedeleteTests()
        {
            _sut = NewspaperCategoryServiceFactory.Create(SetupContext);

        }
        [Fact]
        public async Task Delete_deletes_newspaperCategory_properly()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
            DbContext.Save(category2);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").WithCategory(category2.Id).Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category1.Id, newspaper.Id)
                 .Build();
            DbContext.Save(newspaperCategory);

            await _sut.Delete(newspaperCategory.Id);

            var actual = ReadContext.NewspaperCategories
                .Any(_ => _.NewspaperId == newspaper.Id && _.CategoryId == category2.Id);
            actual.Should().BeTrue();
        }
        [Fact]
        public async Task Delete_throws_NewspaperCategoryIsNotExistException()
        {
            var dummyNewspaperCategoryId = 22;

            var actual = () => _sut.Delete(dummyNewspaperCategoryId);

            await actual.Should().ThrowExactlyAsync<NewspaperCategoryIsNotExistException>();
        }
        
    }
}
