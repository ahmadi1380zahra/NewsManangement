using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Newspapers
{
    public class NewspaperServiceDeleteTests : BusinessUnitTest
    {
        private readonly NewspaperService _sut;
        public NewspaperServiceDeleteTests()
        {
            _sut = NewspaperServiceFactory.Create(SetupContext);

        }
        [Fact]
        public async Task Delete_deletes_a_newspaper_properly()
        {

            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder().WithCategory(category1.Id).Build();
            DbContext.Save(newspaper);

            await _sut.Delete(newspaper.Id);

            var actual = ReadContext.Newspapers.FirstOrDefault(_ => _.Id == newspaper.Id);
            var isAnyCategoryForNewspaper = ReadContext.NewspaperCategories
                .Any(_ => _.NewspaperId == newspaper.Id);
            actual.Should().BeNull();
            isAnyCategoryForNewspaper.Should().BeFalse();
        }
        [Fact]
        public async Task Delete_throws_NewspaperIsNotExistException()
        {
            var dummyNewspaperId = 10;

            var actual=()=> _sut.Delete(dummyNewspaperId);

           await actual.Should().ThrowExactlyAsync<NewspaperIsNotExistException>();
        }
        [Fact]
        public async Task Delete_throws_NewspaperHasBeenPublishedYouCantDeleteIt()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var newspaper = new NewspaperBuilder()
                .WithPublishDate(new DateTime(2024, 3, 12))
                .WithCategory(category1.Id).Build();
            DbContext.Save(newspaper);

            var actual = () => _sut.Delete(newspaper.Id);

            await actual.Should().ThrowExactlyAsync<NewspaperHasBeenPublishedYouCantDeleteIt>();
        }
    }
}
