using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangement.Test.Tools.TheNews;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Authors
{
    public class AuthorServiceGetTests : BusinessUnitTest
    {
        private readonly AuthorService _sut;
        public AuthorServiceGetTests()
        {
            _sut = AuthorServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task Get_gets_an_author_by_Id()
        {
            var author1 = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author1);
            var author2 = new AuthorBuilder().WithFullName("نیلوفر حقیقت").Build();
            DbContext.Save(author2);


            var actual = await _sut.Get(author1.Id);

            actual.Id.Should().Be(author1.Id);
            actual.FullName.Should().Be(author1.FullName);
        }
        [Fact]
        public async Task Get_gets_all_authors_with_valid_data()
        {
            var author1 = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author1);
            var author2 = new AuthorBuilder().WithFullName("نیلوفر حقیقت").Build();
            DbContext.Save(author2);
            var dto = GetAuthorsFilterDtoFactory.Create(null);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(2);
            var author = actual.FirstOrDefault();
            author.Id.Should().Be(author1.Id);
            author.FullName.Should().Be(author1.FullName);
        }
        [Fact]
        public async Task Get_gets_authors_by_filter_with_valid_data()
        {
            var author1 = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author1);
            var author2 = new AuthorBuilder().WithFullName("نیلوفر حقیقت").Build();
            DbContext.Save(author2);
            var filter = "حقیق";
            var dto = GetAuthorsFilterDtoFactory.Create(filter);

            var actual = await _sut.GetAll(dto);

            actual.Count().Should().Be(1);
            var author = actual.FirstOrDefault();
            author.Id.Should().Be(author2.Id);
            author.FullName.Should().Be(author2.FullName);
        }
        [Fact]
        public async Task GetMostViewed_get_most_viewed_author()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var author1 = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author1);
            var author2 = new AuthorBuilder().WithFullName("علی احمدی").Build();
            DbContext.Save(author2);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory);
            var theNew1 = new TheNewBuilder(author2.Id, newspaperCategory.Id)
                .WithTitle("پرسپولیس در لیگ")
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(5)
                .WithView(10)
                .WithTheNewTags(tag1.Id)
             .Build();
            DbContext.Save(theNew1);
            var theNew2 = new TheNewBuilder(author1.Id, newspaperCategory.Id)
             .WithTitle("استقلال در سوگ")
             .WithDesciption("مربی استقلال مرد")
             .WithRate(5)
             .WithView(20)
             .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew2);


            var authors = await _sut.GetMostViewed();

            authors.Count.Should().Be(1);
            var author = authors.FirstOrDefault();
            author.Id.Should().Be(author1.Id);
            author.FullName.Should().Be(author1.FullName);

        }
        [Fact]
        public async Task GetHighestNewsCount_get_authors_that_have_highest_news_count()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var author1 = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author1);
            var author2 = new AuthorBuilder().WithFullName("علی احمدی").Build();
            DbContext.Save(author2);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory);
            var theNew1 = new TheNewBuilder(author2.Id, newspaperCategory.Id)
                .WithTitle("پرسپولیس در لیگ")
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(5)
                .WithView(10)
                .WithTheNewTags(tag1.Id)
             .Build();
            DbContext.Save(theNew1);
            var theNew2 = new TheNewBuilder(author1.Id, newspaperCategory.Id)
             .WithTitle("استقلال در سوگ")
             .WithDesciption("مربی استقلال مرد")
             .WithRate(5)
             .WithView(20)
             .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew2);
            var theNew3 = new TheNewBuilder(author1.Id, newspaperCategory.Id)
          .WithTitle("استقلال در سوگ")
          .WithDesciption("بازیکن استقلال مرد")
          .WithRate(5)
          .WithView(20)
          .WithTheNewTags(tag1.Id)
          .Build();
            DbContext.Save(theNew3);


            var authors = await _sut.GetHighestNewsCount();

            authors.Count.Should().Be(1);
            var author = authors.FirstOrDefault();
            author.Id.Should().Be(author1.Id);
            author.FullName.Should().Be(author1.FullName);

        }
    }
}
