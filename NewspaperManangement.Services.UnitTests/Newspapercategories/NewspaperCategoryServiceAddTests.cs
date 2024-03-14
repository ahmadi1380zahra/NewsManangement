using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using NewspaperManangment.Services.Catgories.Exceptions;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangment.Services.Newspapers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Newspapercategories
{
    public class NewspaperCategoryServiceAddTests : BusinessUnitTest
    {
        private readonly NewspaperCategoryService _sut;
        public NewspaperCategoryServiceAddTests()
        {
            _sut = NewspaperCategoryServiceFactory.Create(SetupContext);

        }
        [Fact]
        public async Task Add_adds_a_new_category_to_a_newspaper_properly()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").WithCategory(category1.Id).Build();
            DbContext.Save(newspaper);
            var category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
            DbContext.Save(category2);
            var dto = AddNewspaperCategoryDtoFactory.Create(category2.Id, newspaper.Id);

            await _sut.Add(dto);

            var newspaperCategory = ReadContext.NewspaperCategories
             .Any(_ => _.NewspaperId == newspaper.Id && _.CategoryId == category2.Id);
            newspaperCategory.Should().BeTrue();
        }
        [Fact]
        public async Task Add_throws_CategoryIsNotExistException()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").WithCategory(category1.Id).Build();
            DbContext.Save(newspaper);
            var dummyCategoryId = 11;
            var dto = AddNewspaperCategoryDtoFactory.Create(dummyCategoryId, newspaper.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CategoryIsNotExistException>();
        }
        [Fact]
        public async Task Add_throws_NewspaperIsNotExistException()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var dummyNewspaperId = 11;
            var dto = AddNewspaperCategoryDtoFactory.Create(category1.Id, dummyNewspaperId);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<NewspaperIsNotExistException>();
        }
        [Fact]
        public async Task Add_throws_CategoryIsReduplicateForThisNewspaperException()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").WithCategory(category1.Id).Build();
            DbContext.Save(newspaper);
            var dto = AddNewspaperCategoryDtoFactory.Create(category1.Id, newspaper.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CategoryIsReduplicateForThisNewspaperException>();
        }
        [Fact]
        public async Task Add_throws_NewspaperHasBeenPublishedYouCantUpdateIt()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع")
                .WithCategory(category1.Id)
                .WithPublishDate(new DateTime(2024,3,14)).Build();
            DbContext.Save(newspaper);
            var dto = AddNewspaperCategoryDtoFactory.Create(category1.Id, newspaper.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<NewspaperHasBeenPublishedYouCantUpdateIt>();
        }
    }
}
