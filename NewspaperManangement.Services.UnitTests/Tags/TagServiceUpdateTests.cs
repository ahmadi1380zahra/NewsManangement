using Azure;
using NewspaperManangement.Test.Tools.Categories;
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

namespace NewspaperManangement.Services.UnitTests.Tags
{
    public class TagServiceUpdateTests : BusinessUnitTest
    {
        private readonly TagService _sut;
        public TagServiceUpdateTests()
        {
            _sut = TagServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Update_updates_a_tag_properly()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var tag = new TagBuilder(category1.Id).WithTitle("ادبیات معاصر").Build();
            DbContext.Save(tag);
            var title = "ادبیات فرهنگی";
            var dto = UpdateTagDtoFactory.Create(category1.Id, title);

            await _sut.Update(tag.Id, dto);

            var actual = ReadContext.Tags.Single();
            actual.Title.Should().Be(dto.Title);
            actual.CategoryId.Should().Be(dto.CategoryId);
        }
        [Fact]
        public async Task Update_throws_TagIsNotExistException()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var title = "ادبیات فرهنگی";
            var dto = UpdateTagDtoFactory.Create(category1.Id, title);
            var dummyId = 10;

            var actual = () => _sut.Update(dummyId, dto);

            await actual.Should().ThrowExactlyAsync<TagIsNotExistException>();
        }
        [Fact]
        public async Task Update_throws_CategoryIsNotExistException()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var tag = new TagBuilder(category1.Id).WithTitle("ادبیات معاصر").Build();
            DbContext.Save(tag);
            var dummyCategoryId = 10;
            var dto = UpdateTagDtoFactory.Create(dummyCategoryId);


            var actual = () => _sut.Update(tag.Id, dto);

            await actual.Should().ThrowExactlyAsync< CategoryIsNotExistException>();
        }
    }
}
