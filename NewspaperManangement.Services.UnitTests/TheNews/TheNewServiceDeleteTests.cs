using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangement.Test.Tools.TheNews;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangment.Services.TheNews.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.TheNews
{
    public class TheNewServiceDeleteTests : BusinessUnitTest
    {
        private readonly TheNewService _sut;
        public TheNewServiceDeleteTests()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Delete_deletes_a_news_properly()
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
              .WithRate(15)
              .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew);

            await _sut.Delete(theNew.Id);

            var news = ReadContext.TheNews.FirstOrDefault(_ => _.Id == theNew.Id);
            news.Should().BeNull();
            var tags = ReadContext.TheNewTags.Any(_ => _.TheNewId == theNew.Id);
            tags.Should().BeFalse();
        }
        [Fact]
        public async Task Delete_Throws_TheNewIsNotExistedException()
        {
            var dummyNewsId = 11;

           var actual=()=>  _sut.Delete(dummyNewsId);

            await actual.Should().ThrowExactlyAsync<TheNewIsNotExistedException>();
        }
        [Fact]
        public async Task Delete_throws_theNewHasBeenPublishedYouCantDeleteItExceotion()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع")
                .WithPublishDate(new DateTime(2024,3,16)).Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory);
            var theNew = new TheNewBuilder(author.Id, newspaperCategory.Id)
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(15)
              .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew);

            var actual = () => _sut.Delete(theNew.Id);

            await actual.Should().ThrowExactlyAsync<TheNewHasBeenPublishedYouCantDeleteItException>();
        }
    }
}
