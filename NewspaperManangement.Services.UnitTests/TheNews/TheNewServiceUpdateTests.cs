using Azure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangment.Services.TheNews.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.TheNews
{
    public class TheNewServiceUpdateTests : BusinessUnitTest
    {
        private readonly TheNewService _sut;
        public TheNewServiceUpdateTests()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Update_updates_a_news_properly()
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
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(10)
              .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew);
            var author2 = new AuthorBuilder().WithFullName("علی رضا احمدی").Build();
            DbContext.Save(author2);
            var dto = UpdateTheNewDtoFactory.Create(author2.Id, newspaperCategory.Id);

            await _sut.Update(theNew.Id, dto);

            var actual = ReadContext.TheNews.FirstOrDefault(_ => _.Id == theNew.Id);
            actual.Title.Should().Be(dto.Title);
            actual.Description.Should().Be(dto.Description);
            actual.Rate.Should().Be(dto.Rate);
            actual.AuthorId.Should().Be(dto.AuthorId);
            actual.NewspaperCategoryId.Should().Be(dto.NewsPaperCategoryId);
        }
        [Fact]
        public async Task Update_throws_TheNewIsNotExistedException()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory);
            var author2 = new AuthorBuilder().WithFullName("علی رضا احمدی").Build();
            DbContext.Save(author2);
            var dto = UpdateTheNewDtoFactory.Create(author2.Id, newspaperCategory.Id);
            var dummyTheNewId = 12;

            var actual = () => _sut.Update(dummyTheNewId, dto);

            await actual.Should().ThrowExactlyAsync<TheNewIsNotExistedException>();

        }
        [Fact]
        public async Task Update_throws_AuthorIsNotExistException()
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
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(10)
              .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew);
            var dummyAuthorId = 12;
            var dto = UpdateTheNewDtoFactory.Create(dummyAuthorId, newspaperCategory.Id);

            var actual = () => _sut.Update(theNew.Id, dto);

            await actual.Should().ThrowExactlyAsync<AuthorIsNotExistException>();
        }
        [Fact]
        public async Task Update_throws_NewsRateShouldBeMoreThanZeroException()
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
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(10)
              .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew);
            var dummyRate = 0;
            var dto = UpdateTheNewDtoFactory.Create(author.Id, newspaperCategory.Id, dummyRate);

            var actual = () => _sut.Update(theNew.Id, dto);

            await actual.Should().ThrowExactlyAsync<NewsRateShouldBeMoreThanZeroException>();
        }
        [Fact]
        public async Task Update_throws_NewspaperCategoryIsNotExistException()
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
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(10)
              .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew);
            var dummyNewspaperCategoryId = 10;
            var dto = UpdateTheNewDtoFactory.Create(author.Id, dummyNewspaperCategoryId);

            var actual = () => _sut.Update(theNew.Id, dto);

            await actual.Should().ThrowExactlyAsync<NewspaperCategoryIsNotExistException>();
        }
        [Fact]
        public async Task Update_throws_TheNewspaperCategoryIsFullException()
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
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(10)
              .WithTheNewTags(tag1.Id)
              .Build();
            DbContext.Save(theNew);
            var theNew1 = new TheNewBuilder(author.Id, newspaperCategory.Id)
             .WithTitle("سپاهان در لیگ")
             .WithDesciption("سپاهان از لیگ حذف شد")
             .WithRate(10)
             .WithTheNewTags(tag1.Id)
             .Build();
            DbContext.Save(theNew1);
            var dto = UpdateTheNewDtoFactory.Create(author.Id, newspaperCategory.Id, 15);

            var actual = () => _sut.Update(theNew.Id, dto);

            await actual.Should().ThrowExactlyAsync<TheNewspaperCategoryIsFullException>();
        }
        [Fact]
        public async Task Update_throws_TheNewsRateCantBeEqualToCategoryRateItShouldBeLessException()
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
            .Build();
            DbContext.Save(theNew);
            var dto = UpdateTheNewDtoFactory.Create(author.Id, newspaperCategory.Id, 20);

            var actual = () => _sut.Update(theNew.Id, dto);

            await actual.Should().ThrowExactlyAsync<TheNewsRateCantBeEqualToCategoryRateItShouldBeLessException>();
        }
    }
}
