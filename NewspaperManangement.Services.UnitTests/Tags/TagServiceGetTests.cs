using Azure;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Tags.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NewspaperManangment.Services.Tags.Exceptions;
using NewspaperManangment.Services.Catgories.Exceptions;
using NewspaperManangement.Test.Tools.Categories;

namespace NewspaperManangement.Services.UnitTests.Tags
{
    public class TagServiceGetTests : BusinessUnitTest
    {
        private readonly TagService _sut;
        public TagServiceGetTests()
        {
            _sut = TagServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Get_gets_a_tag_properly()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var tag1 = new TagBuilder(category1.Id).WithTitle("تجاوز").Build();
            DbContext.Save(tag1);
            var tag2 = new TagBuilder(category1.Id).WithTitle("قتل").Build();
            DbContext.Save(tag2);
            var tag3 = new TagBuilder(category1.Id).WithTitle("غارت").Build();
            DbContext.Save(tag3);
            var dto = GetTagFilterDtoFactory.Create(null);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(3);
        }
        [Fact]
        public async Task Get_gets_tags_by_title_filter()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var tag1 = new TagBuilder(category1.Id).WithTitle("تجاوز").Build();
            DbContext.Save(tag1);
            var tag2 = new TagBuilder(category1.Id).WithTitle("قتل").Build();
            DbContext.Save(tag2);
            var tag3 = new TagBuilder(category1.Id).WithTitle("غارت").Build();
            DbContext.Save(tag3);
            var filter = "ا";
            var dto = GetTagFilterDtoFactory.Create(filter);

            var tags = await _sut.GetAll(dto);

            tags.Count().Should().Be(2);
            var actual = tags.FirstOrDefault();
            actual.Id.Should().Be(tag1.Id);
            actual.Title.Should().Be(tag1.Title);
            actual.Category.Should().Be(tag1.Category.Title);
        }
        [Fact]
        public async Task Get_gets_tag_and_check_for_valid_data()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var tag1 = new TagBuilder(category1.Id).WithTitle("تجاوز").Build();
            DbContext.Save(tag1);
            var dto = GetTagFilterDtoFactory.Create(null);

            var tags = await _sut.GetAll(dto);

            tags.Count().Should().Be(1);
            var actual = tags.FirstOrDefault();
            actual.Id.Should().Be(tag1.Id);
            actual.Title.Should().Be(tag1.Title);
            actual.Category.Should().Be(tag1.Category.Title);
        }
    }
}
