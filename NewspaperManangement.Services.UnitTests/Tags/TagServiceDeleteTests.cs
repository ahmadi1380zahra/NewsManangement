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
    public class TagServiceDeleteTests : BusinessUnitTest
    {
        private readonly TagService _sut;
        public TagServiceDeleteTests()
        {
            _sut = TagServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Delete_deletes_a_tag_properly()
        {
            var category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(category1);
            var tag = new TagBuilder(category1.Id).WithTitle("تجاوز").Build();
            DbContext.Save(tag);
        
            await _sut.Delete(tag.Id);

            var actual = ReadContext.Tags.FirstOrDefault(_ => _.Id == tag.Id);
            actual.Should().BeNull();
        }
        [Fact]
        public async Task Delete_throws_TagIsNotExistException()
        {
           var dummyId = 10;

            var actual = () => _sut.Delete(dummyId);

            await actual.Should().ThrowExactlyAsync<TagIsNotExistException>();
        }
   
    }
}
