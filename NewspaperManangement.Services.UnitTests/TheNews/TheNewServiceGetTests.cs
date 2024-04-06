using FluentAssertions;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
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
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.TheNews
{
    public class TheNewServiceGetTests : BusinessUnitTest
    {
        private readonly TheNewService _sut;
        public TheNewServiceGetTests()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task GetAll_gets_all_theNews_with_valid_data()
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
            var theNew1 = new TheNewBuilder(author.Id, newspaperCategory.Id)
                .WithTitle("پرسپولیس در لیگ")
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(15)
                .WithTheNewTags(tag1.Id)
             .Build();
            DbContext.Save(theNew1);
            var theNew2 = new TheNewBuilder(author.Id, newspaperCategory.Id)
       .WithTitle("پرسپولیس در اسیا")
       .WithDesciption("پرسپولیس قهرمان اسیا شد")
       .WithRate(15)
       .WithTheNewTags(tag1.Id)
    .Build();
            DbContext.Save(theNew2);
            var dto = GetTheNewFilterDtoFactory.Create(null);

            var actual =await _sut.GetAll(dto);

            var theNew = actual.FirstOrDefault(_ => _.Id == theNew1.Id);
            theNew.Id.Should().Be(theNew1.Id);
            theNew.Title.Should().Be(theNew1.Title);
            theNew.Description.Should().Be(theNew1.Description);
            theNew.AuthorName.Should().Be(theNew1.Author.FullName);
            theNew.Rate.Should().Be(theNew1.Rate);
            theNew.View.Should().Be(theNew1.View);
            theNew.NewspaperCategoryId.Should().Be(theNew1.NewspaperCategoryId);
            var theNewSecond = actual.FirstOrDefault(_ => _.Id == theNew2.Id);
            theNewSecond.Id.Should().Be(theNew2.Id);
            theNewSecond.Title.Should().Be(theNew2.Title);
            theNewSecond.Description.Should().Be(theNew2.Description);
            theNewSecond.AuthorName.Should().Be(theNew2.Author.FullName);
            theNewSecond.Rate.Should().Be(theNew2.Rate);
            theNewSecond.View.Should().Be(theNew2.View);
            theNewSecond.NewspaperCategoryId.Should().Be(theNew2.NewspaperCategoryId);
        }
        [Theory]
        [InlineData("پرسپولیس در لیگ", "پرس")]
        public async Task GetAll_gets_theNews_with_valid_data_by_title_filter(string title,string filter)
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
            var theNew1 = new TheNewBuilder(author.Id, newspaperCategory.Id)
                .WithTitle(title)
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(15)
                .WithTheNewTags(tag1.Id)
             .Build();
            DbContext.Save(theNew1);
            var theNew2 = new TheNewBuilder(author.Id, newspaperCategory.Id)
       .WithTitle("استقلال در اسیا")
       .WithDesciption("استقلال باخت")
       .WithRate(15)
       .WithTheNewTags(tag1.Id)
    .Build();
            DbContext.Save(theNew2);
            var dto = GetTheNewFilterDtoFactory.Create(filter);

            var actual = await _sut.GetAll(dto);

            var theNew = actual.FirstOrDefault(_ => _.Id == theNew1.Id);
            theNew.Id.Should().Be(theNew1.Id);
            theNew.Title.Should().Be(theNew1.Title);
            theNew.Description.Should().Be(theNew1.Description);
            theNew.AuthorName.Should().Be(theNew1.Author.FullName);
            theNew.Rate.Should().Be(theNew1.Rate);
            theNew.View.Should().Be(theNew1.View);
            theNew.NewspaperCategoryId.Should().Be(theNew1.NewspaperCategoryId);

        }
        [Fact]
        public async Task Get_get_a_news_to_increase_view()
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
            var theNew1 = new TheNewBuilder(author.Id, newspaperCategory.Id)
                .WithTitle("پرسپولیس در لیگ")
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(15)
                .WithTheNewTags(tag1.Id)
             .Build();
            DbContext.Save(theNew1);

            var theNew = await _sut.GetToIncreaseView(theNew1.Id);

            theNew.Id.Should().Be(theNew1.Id);
            theNew.Title.Should().Be(theNew1.Title);
            theNew.Description.Should().Be(theNew1.Description);
            theNew.AuthorName.Should().Be(theNew1.Author.FullName);
            theNew.Rate.Should().Be(theNew1.Rate);

        }
        [Fact]
        public async Task GetMostViewd_get_most_viewed_news()
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
            var theNew1 = new TheNewBuilder(author.Id, newspaperCategory.Id)
                .WithTitle("پرسپولیس در لیگ")
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(5)
              .WithView(10)
                .WithTheNewTags(tag1.Id)
             .Build();
            DbContext.Save(theNew1);
            var theNew2 = new TheNewBuilder(author.Id, newspaperCategory.Id)
             .WithTitle("استقلال در سوگ")
             .WithDesciption("مربی استقلال مرد")
             .WithRate(5)
             .WithView(1)
             .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew2);
            var theNew3 = new TheNewBuilder(author.Id, newspaperCategory.Id)
             .WithTitle("پرسپولیس در سوگ")
             .WithDesciption("امیری و ترابی مردند")
             .WithRate(5)
             .WithTheNewTags(tag1.Id)
             .WithView(10)
             .Build();
            DbContext.Save(theNew3);

            var TheNews = await _sut.GetMostViewd();

            TheNews.Count.Should().Be(2);
            var firstNews = TheNews.FirstOrDefault(_ => _.Id == theNew1.Id);
            firstNews.Title.Should().Be(theNew1.Title);
            firstNews.Description.Should().Be(theNew1.Description);
            firstNews.AuthorName.Should().Be(theNew1.Author.FullName);
            firstNews.Rate.Should().Be(theNew1.Rate);
            firstNews.View.Should().Be(theNew1.View);
            var secondNews = TheNews.FirstOrDefault(_ => _.Id == theNew3.Id);
            secondNews.Title.Should().Be(theNew3.Title);
            secondNews.Description.Should().Be(theNew3.Description);
            secondNews.AuthorName.Should().Be(theNew3.Author.FullName);
            secondNews.Rate.Should().Be(theNew3.Rate);
            secondNews.View.Should().Be(theNew3.View);
        }
    }
}
