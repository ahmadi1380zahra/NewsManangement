using Azure;
using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangement.Test.Tools.TheNews;
using NewspaperManangment.Entities;
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
    public class NewspaperServiceUpdateTests : BusinessUnitTest
    {
        private readonly NewspaperService _sut;
        public NewspaperServiceUpdateTests()
        {
            _sut = NewspaperServiceFactory.Create(SetupContext);

        }
        [Fact]
        public async Task Update_update_a_new_newspaper_Title_properly()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder().WithCategory(category1.Id).Build();
            DbContext.Save(newspaper);
            var dto = UpdateNewsPaperDtoFactory.Create("انقلاب");

            await _sut.Update(newspaper.Id, dto);

            var actual = ReadContext.Newspapers.FirstOrDefault(_ => _.Id == newspaper.Id);
            actual.Title.Should().Be("انقلاب");
            var newspaperCategory = ReadContext.NewspaperCategories.FirstOrDefault(_ => _.NewspaperId == newspaper.Id);
            newspaperCategory.CategoryId.Should().Be(category1.Id);
        }
        [Fact]
        public async Task Update_throws_NewspaperIsNotExistException()
        {
            var dummyNewspaperId = 12;
            var dto = UpdateNewsPaperDtoFactory.Create("انقلاب");

            var actual = () => _sut.Update(dummyNewspaperId, dto);

            await actual.Should().ThrowExactlyAsync<NewspaperIsNotExistException>();
        }
        [Fact]
        public async Task Update_throws_NewspaperHasBeenPublishedYouCantUpdateIt()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder()
                .WithPublishDate(new DateTime(2024 / 10 / 2))
                .WithCategory(category1.Id).Build();
            DbContext.Save(newspaper);
            var dto = UpdateNewsPaperDtoFactory.Create("انقلاب");

            var actual = () => _sut.Update(newspaper.Id, dto);

            await actual.Should().ThrowExactlyAsync<NewspaperHasBeenPublishedYouCantUpdateIt>();
        }
    }
}
