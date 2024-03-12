using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using FluentAssertions;

namespace NewspaperManangement.Services.UnitTests.Categories
{
    public class CategoryServiceGetTests : BusinessUnitTest
    {
        private readonly CategoryService _sut;
        public CategoryServiceGetTests()
        {
            _sut = CategoryServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Get_gets_all_categories()
        {
            var category1 = new CategoryBuilder().Build();
            DbContext.Save(category1);
            var category2 = new CategoryBuilder().Build();
            DbContext.Save(category2);
            var category3 = new CategoryBuilder().Build();
            DbContext.Save(category3);
            var dto = GetCategoryFilterDtoFactory.Create(null);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(3);
        }
        [Fact]
        public async Task Get_gets_catgories_by_title_filter()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
            DbContext.Save(category2);
            var category3 = new CategoryBuilder().WithTitle("فرهنگی").Build();
            DbContext.Save(category3);
            var filter = "فرهن";
            var dto = GetCategoryFilterDtoFactory.Create(filter);

            var catgeories = await _sut.GetAll(dto);

            catgeories.Count().Should().Be(1);
            var actual = catgeories.FirstOrDefault();
            actual.Id.Should().Be(category3.Id);
            actual.Title.Should().Be(category3.Title);
            actual.Rate.Should().Be(category3.Rate);
        }
        [Fact]
        public async Task Get_gets_catgories_and_check_for_valid_data()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var dto = GetCategoryFilterDtoFactory.Create(null);

            var catgeories = await _sut.GetAll(dto);

            catgeories.Count().Should().Be(1);
            var actual = catgeories.FirstOrDefault();
            actual.Id.Should().Be(category1.Id);
            actual.Title.Should().Be(category1.Title);
            actual.Rate.Should().Be(category1.Rate);
        }
    }
}