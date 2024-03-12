using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using NewspaperManangment.Services.Catgories.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Categories
{
    public class CategoryServiceAddTests : BusinessUnitTest
    {
        private readonly CategoryService _sut;
        public CategoryServiceAddTests()
        {
            _sut = CategoryServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Add_adds_a_new_Category_properly()
        {
            var dto = AddCategoryDtoFactory.Create();

            await _sut.Add(dto);

            var actual = ReadContext.Categories.Single();
            actual.Title.Should().Be(dto.Title);
            actual.Rate.Should().Be(dto.Rate);
        }
        [Fact]
        public async Task Add_throws_RateShouldBeMoreThanZeroException()
        {
            var dummyRate = -10;
            var dto = AddCategoryDtoFactory.Create(dummyRate);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CategoryRateShouldBeMoreThanZeroException>();
        }
    }
}
