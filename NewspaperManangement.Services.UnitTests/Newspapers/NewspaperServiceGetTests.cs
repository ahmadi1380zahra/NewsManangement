using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Newspapers
{
    public class NewspaperServiceGetTests : BusinessUnitTest
    {
        private readonly NewspaperService _sut;
        public NewspaperServiceGetTests()
        {
            _sut = NewspaperServiceFactory.Create(SetupContext);

        }
        [Fact]
        public async Task GetAll_gets_all_newspaper_with_valid_data()
        {
            var newspaper1 = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper1);
            var newspaper2 = new NewspaperBuilder().WithTitle("افتاب")
                .WithPublishDate(new DateTime(2024, 2, 2).Date).Build();
            DbContext.Save(newspaper2);
            var dto = GetNewspaperFilterDtoFactory.Create(null);

            var actual = await _sut.GetAll(dto);

            actual.Count.Should().Be(2);
            var news1 = actual.FirstOrDefault(_ => _.Id == newspaper1.Id);
            news1.Title.Should().Be(newspaper1.Title);
            news1.Status.Should().Be("منتشر نشده");
           
        }
        [Fact]
        public async Task GetAll_gets_newspapers_by_title_filter()
        {
            var newspaper1 = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper1);
            var newspaper2 = new NewspaperBuilder().WithTitle("افتاب")
             .Build();
            DbContext.Save(newspaper2);
            var dto = GetNewspaperFilterDtoFactory.Create("افت");

            var actual = await _sut.GetAll(dto);

            actual.Count.Should().Be(1);
            var news1 = actual.FirstOrDefault(_ => _.Id == newspaper2.Id);
            news1.Title.Should().Be(newspaper2.Title);
            news1.Status.Should().Be("منتشر نشده");

        }
    }
}
