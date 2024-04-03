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
using NewspaperManangment.Services.Tags.Exceptions;
using NewspaperManangment.Services.TheNews.Exceptions;
using NewspaperManangment.Services.TheNewTags.Contracts;
using NewspaperManangment.Services.TheNewTags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.TheNewTags
{
    public class TheNewTagServiceAddTests : BusinessUnitTest
    {
        private readonly TheNewTagService _sut;
        public TheNewTagServiceAddTests()
        {
            _sut = TheNewTagServiceFactory.Create(SetupContext);

        }
        [Fact]
        public async Task Add_adds_a_new_tag_to_news_properly()
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
            var dto = AddTheNewTagDtoFactory.Create(theNew.Id, tag2.Id);

            await _sut.Add(dto);

            var theNewTags = ReadContext.TheNewTags.Where(_ => _.TheNewId == theNew.Id);
            theNewTags.Count().Should().Be(2);
            theNewTags.Any(_ => _.TagId == tag1.Id).Should().BeTrue();
            theNewTags.Any(_ => _.TagId == tag2.Id).Should().BeTrue();
        }
        [Fact]
        public async Task Add_adds_throws_TagIsNotExistException()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
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
            var dummyTagId = 12;
            var dto = AddTheNewTagDtoFactory.Create(theNew.Id, dummyTagId);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<TagIsNotExistException>();
        }
        [Fact]
        public async Task Add_adds_throws_TheNewIsNotExistException()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var dummytheNewId = 12;
            var dto = AddTheNewTagDtoFactory.Create(dummytheNewId, tag1.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<TheNewIsNotExistedException>();
        }
        [Fact]
        public async Task Add_throws_ThisTagIsNotAllowedForThisNewspaperCategoryException()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var category2 = new CategoryBuilder().WithRate(20).WithTitle("جنایی").Build();
            DbContext.Save(category2);
            var tag2 = new TagBuilder(category2.Id).WithTitle("فوتیال در سوگ").Build();
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
            var dto = AddTheNewTagDtoFactory.Create(theNew.Id, tag2.Id);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<ThisTagIsNotAllowedForThisNewspaperCategoryException>();
        }
    }
}
