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
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Newspapers
{
    public class NewspaperServicePublishTests:BusinessUnitTest
    {
        private readonly NewspaperService _sut;
        private readonly DateTime _fakeTime;
        public NewspaperServicePublishTests()
        {
            _fakeTime = new DateTime(2018, 2, 4);
            _sut = NewspaperServiceFactory.Create(SetupContext, _fakeTime);

        }
        [Fact]
        public async Task Publish_update_newspaper_PublishDate_AfterBeingPublished_properly()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var category2 = new CategoryBuilder().WithRate(10).WithTitle("جنایی").Build();
            DbContext.Save(category2);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var tag2 = new TagBuilder(category2.Id).WithTitle("قتل").Build();
            DbContext.Save(tag2);
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory);
            var newspaperCategory2 = new NewspaperCategoryBuilder(category2.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory2);
            var theNew1 = new TheNewBuilder(author.Id, newspaperCategory.Id)
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(15)
              .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew1);
            var theNew2 = new TheNewBuilder(author.Id, newspaperCategory.Id)
            .WithTitle("استقلال در سوگ")
            .WithDesciption(" بازیکن استقلال مرد")
            .WithRate(5)
            .WithTheNewTags(tag1.Id)
          .Build();
            DbContext.Save(theNew2);
            var theNew3 = new TheNewBuilder(author.Id, newspaperCategory2.Id)
       .WithTitle("استقلال بمیره")
       .WithDesciption(" استقلال کوشته شد")
       .WithRate(5)
       .WithTheNewTags(tag2.Id)
     .Build();
            DbContext.Save(theNew3);
            var theNew4 = new TheNewBuilder(author.Id, newspaperCategory2.Id)
            .WithTitle("سپاهان بمیره")
            .WithDesciption(" سپاهان کوشته شد")
            .WithRate(5)
            .WithTheNewTags(tag2.Id)
           .Build();
            DbContext.Save(theNew4);

            await _sut.Publish(newspaper.Id);

            var actual = ReadContext.Newspapers.FirstOrDefault(_ => _.Id == newspaper.Id);
            actual.PublishDate.Should().Be(_fakeTime);
        }
        [Fact]
        public async Task Publish_throws_NewspaperIsNotCompeletedToBePublishedException()
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

            var actual = () => _sut.Publish(newspaper.Id);

            await actual.Should().ThrowExactlyAsync<NewspaperIsNotCompeletedToBePublishedException>();

        }
        [Fact]
        public async Task Publish_throws_NewspaperIsNotExistException()
        {
            var dummyNewspaperId = 12;

            var actual = () => _sut.Publish(dummyNewspaperId);

            await actual.Should().ThrowExactlyAsync<NewspaperIsNotExistException>();
        }
        [Fact]
        public async Task Publish_throws_NewspaperIsAlreadyPublishedException()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع")
                .WithPublishDate(new DateTime(2024,2,3))
                .Build();
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
            .WithTitle("پرسپولیس در لیگ")
            .WithDesciption("پرسپولیس قهرمان لیگ شد")
            .WithRate(5)
            .WithTheNewTags(tag1.Id)
          .Build();
            DbContext.Save(theNew2);

            var actual = () => _sut.Publish(newspaper.Id);

            await actual.Should().ThrowExactlyAsync<NewspaperIsAlreadyPublishedException>();
        }
    }
}
