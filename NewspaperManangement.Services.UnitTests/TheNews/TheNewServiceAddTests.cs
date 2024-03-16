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
using NewspaperManangment.Services.Authors.Exceptions;
using NewspaperManangment.Services.NewspaperCategories.Exceptions;
using NewspaperManangment.Services.Tags.Exceptions;
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using NewspaperManangment.Services.TheNews.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.TheNews
{
    public class TheNewServiceAddTests : BusinessUnitTest
    {
        private readonly TheNewService _sut;
        public TheNewServiceAddTests()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Add_adds_a_new_news_properly()
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
            var dto = new AddTheNewDtoBuilder(author.Id, newspaperCategory.Id)
                .WithTagId(tag1.Id)
                .WithTagId(tag2.Id)
                .Build();

            await _sut.Add(dto);

            var newspaper1 = ReadContext.Newspapers.FirstOrDefault(_ => _.Id == newspaper.Id);
            var news = ReadContext.TheNews.FirstOrDefault();
            var newstags = ReadContext.TheNewTags.Count(_ => _.TheNewId == news.Id);
            news.Title.Should().Be(dto.Title);
            news.Description.Should().Be(dto.Description);
            news.Rate.Should().Be(10);
            news.AuthorId.Should().Be(author.Id);
            news.NewspaperCategoryId.Should().Be(newspaperCategory.Id);
            newstags.Should().Be(2);
        }
        [Fact]
        public async Task Add_throws_TheNewspaperCategoryIsFullException()
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
                .WithTitle("پرسپولیس در لیگ")
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(15)
                .WithTheNewTags(tag1.Id)
                .WithTheNewTags(tag2.Id)
                .Build();
            DbContext.Save(theNew);
            var dto = new AddTheNewDtoBuilder(author.Id, newspaperCategory.Id)
                      .WithTagId(tag1.Id)
                      .WithTagId(tag2.Id)
                      .WithRate(18)
                      .Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<TheNewspaperCategoryIsFullException>();
        }
        [Fact]
        public async Task Add_throws_TheNewsRateCantBeEqualToCategoryRateItShouldBeLessException()
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
            var dto = new AddTheNewDtoBuilder(author.Id, newspaperCategory.Id)
                     .WithTagId(tag1.Id)
                     .WithTagId(tag2.Id)
                     .WithRate(20)
                     .Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<TheNewsRateCantBeEqualToCategoryRateItShouldBeLessException>();

        }
        [Fact]
        public async Task Add_throws_AuthorIsNotExistException()
        {
            var dummyAuthorId = 111;
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var tag2 = new TagBuilder(category.Id).WithTitle("فوتبال چمن").Build();
            DbContext.Save(tag2);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory);
            var dto = new AddTheNewDtoBuilder(dummyAuthorId, newspaperCategory.Id)
                .WithTagId(tag1.Id)
                .WithTagId(tag2.Id)
                .Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<AuthorIsNotExistException>();
        }
        [Fact]
        public async Task Add_throws_NewspaperCategoryIsNotExistException()
        {
            var dummyNewspaperCategoryId = 111;
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var tag2 = new TagBuilder(category.Id).WithTitle("فوتبال چمن").Build();
            DbContext.Save(tag2);
            var dto = new AddTheNewDtoBuilder(author.Id, dummyNewspaperCategoryId)
                         .WithTagId(tag1.Id)
                         .WithTagId(tag2.Id)
                         .Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<NewspaperCategoryIsNotExistException>();
        }
        [Fact]
        public async Task Add_throws_TagIsNotExistException()
        {
            var dummyTagId1 = 11;
            var dummyTagId2 = 10;
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
            var dto = new AddTheNewDtoBuilder(author.Id, newspaperCategory.Id)
                .WithTagId(dummyTagId1)
                .WithTagId(dummyTagId2)
                .Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<TagIsNotExistException>();
        }
        [Fact]
        public async Task Add_throws_ThisTagIsNotAllowedForThisNewspaperCategoryException()
        {
            var category1 = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category1);
            var tag1 = new TagBuilder(category1.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category1.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory);
            var category2 = new CategoryBuilder().WithRate(20).WithTitle("جنایی").Build();
            DbContext.Save(category2);
            var tag2 = new TagBuilder(category2.Id).WithTitle("قتل").Build();
            DbContext.Save(tag2);
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var dto = new AddTheNewDtoBuilder(author.Id, newspaperCategory.Id)
                       .WithTagId(tag1.Id)
                       .WithTagId(tag2.Id)
                       .Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<ThisTagIsNotAllowedForThisNewspaperCategoryException>();
        }
        [Fact]
        public async Task Add_throws_NewsRateShouldBeMoreThanZeroException()
        {
            var dummyRate = 0;
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
            var dto = new AddTheNewDtoBuilder(author.Id, newspaperCategory.Id)
                .WithTagId(tag1.Id)
                .WithTagId(tag2.Id)
                .WithRate(dummyRate)
                .Build();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<NewsRateShouldBeMoreThanZeroException>();
        }
    }
}
