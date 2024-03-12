using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NewspaperManangement.Services.UnitTests.Categories
{
    public class CategoryServiceUpdateTests : BusinessUnitTest
    {
        private readonly CategoryService _sut;
        public CategoryServiceUpdateTests()
        {
            _sut = CategoryServiceFactory.Create(SetupContext);
        }

        [Fact]
        public async Task Update_updates_a_new_Category_properly()
        {
            var category = new CategoryBuilder()
            .WithTitle("جنایی")
            .WithRate(20)
            .Build();
             DbContext.Save(category);
            var updatedTitle = "Updated-title";
            var updatedRate = 15;
            var dto = UpdateCategoryDtoFactory.Create(updatedTitle,updatedRate);

            await _sut.Update(category.Id, dto);

            var actual = ReadContext.Categories.Single();
            actual.Title.Should().Be(dto.Title);
            actual.Rate.Should().Be(dto.Rate);
        }
        [Fact]
        public async Task Update_throws_RateShouldBeMoreThanZeroException()
        {
            var category = new CategoryBuilder()
              .WithTitle("جنایی")
              .WithRate(20)
              .Build();
            DbContext.Save(category);
            var dummyRate = 0;
            var dto = UpdateCategoryDtoFactory.Create(rate:dummyRate);

            var actual = () => _sut.Update(category.Id,dto);

            await actual.Should().ThrowExactlyAsync<CategoryRateShouldBeMoreThanZeroException>();
        }
        [Fact]
        public async Task Update_throws_CategoryIsNotExistException()
        {
            var dummyCategoryId = 32;
            var dto = UpdateCategoryDtoFactory.Create( );

            var actual = () => _sut.Update(dummyCategoryId, dto);

            await actual.Should().ThrowExactlyAsync<CategoryIsNotExistException>();
        }
    }
}
