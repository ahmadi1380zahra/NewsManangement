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
using NewspaperManangment.Services.Catgories.Exceptions;

namespace NewspaperManangement.Services.UnitTests.Tags
{
    public class TagServiceAddTests : BusinessUnitTest
    {
        private readonly TagService _sut;
        public TagServiceAddTests()
        {
            _sut = TagServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Add_adds_a_new_tag_properly()
        {
            var category = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category);
            var dto = AddTagDtoFactory.Create(category.Id);

            await _sut.Add(dto);

            var actual = ReadContext.Tags.FirstOrDefault();
            actual.Title.Should().Be(dto.Title);
            actual.CategoryId.Should().Be(dto.CategoryId);
        }
        [Fact]
        public async Task Add_throw_CategoryIsNotExistException()
        {
            var dummyCategoryId = 18;
            var dto = AddTagDtoFactory.Create(dummyCategoryId);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CategoryIsNotExistException>();
        }
    }
}
