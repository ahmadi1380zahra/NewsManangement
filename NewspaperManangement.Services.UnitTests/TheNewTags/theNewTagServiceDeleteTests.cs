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
using NewspaperManangement.Test.Tools.TheNewTags;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNewTags.Contracts;
using NewspaperManangment.Services.TheNewTags.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.TheNewTags
{
    public class theNewTagServiceDeleteTests:BusinessUnitTest
    {
        private readonly TheNewTagService _sut;
        public theNewTagServiceDeleteTests()
        {
            _sut = TheNewTagServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Delete_deletes_theNewTagProperly()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var tag2 = new TagBuilder(category.Id).WithTitle("فوتبال چمن").Build();
            DbContext.Save(tag2);
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory);
            var theNew = new TheNewBuilder(author.Id, newspaperCategory.Id)
         .WithTitle("پرسپولیس در اسیا")
         .WithDesciption("پرسپولیس قهرمان اسیا شد")
         .WithRate(10)
         .WithTheNewTags(tag1.Id)
       .Build();
            DbContext.Save(theNew);
            var theNewTag = new TheNewTagBuilder(theNew.Id, tag2.Id).Build();
            DbContext.Save(theNewTag);

            await _sut.Delete(theNewTag.Id);

            var theNewTags = ReadContext.TheNewTags.Where(_ => _.TheNewId == theNew.Id);
            theNewTags.Count().Should().Be(1);
            theNewTags.Any(_ => _.TagId == tag1.Id).Should().BeTrue();
        }
        [Fact]
        public async Task Delete_throws_TheNewTagIsNotExistException()
        {
            var dummyTheNewTagId = 12;

           var actual =()=> _sut.Delete(dummyTheNewTagId);

           await actual.Should().ThrowExactlyAsync<TheNewTagIsNotExistException>();
        }
    }
}
