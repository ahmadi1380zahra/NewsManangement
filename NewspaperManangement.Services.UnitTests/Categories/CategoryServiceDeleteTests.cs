using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NewspaperManangment.Services.Catgories.Exceptions;

namespace NewspaperManangement.Services.UnitTests.Categories
{
    public class CategoryServiceDeleteTests: BusinessUnitTest
    {
        private readonly CategoryService _sut;
        public CategoryServiceDeleteTests()
        {
            _sut = CategoryServiceFactory.Create(SetupContext);
        }
      
        [Fact]
        public async Task Delete_deletes_a_category_properly()
        {
            var category = new CategoryBuilder().Build();
            DbContext.Save(category);

            await _sut.Delete(category.Id);

            var actual = ReadContext.Categories.FirstOrDefault(_ => _.Id == category.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_CategoryIsNotExistException()
        {
            var dummyCategoryId = 32;
      
            var actual = () => _sut.Delete(dummyCategoryId);

            await actual.Should().ThrowExactlyAsync<CategoryIsNotExistException>();
        }
    }
}
