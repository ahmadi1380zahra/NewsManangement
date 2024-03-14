using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Catgories.Exceptions;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using NewspaperManangment.Services.Newspapers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Newspapers
{
    public class NewspaperServiceAddTests : BusinessUnitTest
    {
        private readonly NewspaperService _sut;
        public NewspaperServiceAddTests()
        {
            _sut = NewspaperServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Add_adds_a_new_newspaper_properly()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
            DbContext.Save(category2);
            var category3 = new CategoryBuilder().WithTitle("فرهنگی").Build();
            DbContext.Save(category3);
            var dto = AddNewsPaperDtoFactory.Create(category1.Id
                , category2.Id
                , category3.Id);

            await _sut.Add(dto);

            var newspaper = ReadContext.Newspapers.FirstOrDefault();
            var newspaperCategory = ReadContext.NewspaperCategories.FirstOrDefault();
            newspaper.Title.Should().Be(dto.Title);
            newspaperCategory.CategoryId.Should().Be(category1.Id);

        }
        [Fact]
        public async Task Add_throws_NewspaperShouldHaveUniqueNameException()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder()
                 .WithTitle("طلوع")
                 .WithCategory(category1.Id).Build();
            DbContext.Save(newspaper);
            var category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
            DbContext.Save(category2);
            var category3 = new CategoryBuilder().WithTitle("فرهنگی").Build();
            DbContext.Save(category3);
            var dto = AddNewsPaperDtoFactory.Create(category1.Id
                , category2.Id
                , category3.Id
                ,"طلوع");

          var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<NewspaperShouldHaveUniqueNameException>();

        }
        [Fact]
        public async Task Add_throws_CategoryIsNotExistException()
        {
            var dummyCategory1 = 10;
            var dummyCategory2 = 13;
            var dummyCategory3 = 9;

            var dto = AddNewsPaperDtoFactory.Create(dummyCategory1
                , dummyCategory2, dummyCategory3);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CategoryIsNotExistException>();

        }
        [Fact]
        public async Task Add_throws_CategoryIsReduplicateForThisNewspaperException()
        {
            var category = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category);
            var categoryReduplicate = new CategoryBuilder().WithTitle("فرهنگی").Build();
            DbContext.Save(categoryReduplicate);

            var dto = AddNewsPaperDtoFactory.Create(category.Id
                , categoryReduplicate.Id, categoryReduplicate.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CategoryIsReduplicateForThisNewspaperException>();

        }

    }
}
