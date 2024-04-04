using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangement.Test.Tools.TheNews;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.Newspapercategories
{
    public class NewspaperCategoryServiceGetTests : BusinessUnitTest
    {
        private readonly NewspaperCategoryService _sut;
        public NewspaperCategoryServiceGetTests()
        {
            _sut = NewspaperCategoryServiceFactory.Create(SetupContext);
        }
        [Fact]
        public async Task GetHighestNewsCount_gets_category_with_highest_news_count()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory1 = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory1);
            var newspaperCategory2 = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory2);
            var theNew1 = new TheNewBuilder(author.Id, newspaperCategory1.Id)
                .WithTitle("پرسپولیس در لیگ")
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(5)
              .WithView(10)
                .WithTheNewTags(tag1.Id)
             .Build();
            DbContext.Save(theNew1);
            var theNew2 = new TheNewBuilder(author.Id, newspaperCategory1.Id)
             .WithTitle("استقلال در سوگ")
             .WithDesciption("مربی استقلال مرد")
             .WithRate(5)
             .WithView(1)
             .WithTheNewTags(tag1.Id)
            .Build();
            DbContext.Save(theNew2);
            var theNew3 = new TheNewBuilder(author.Id, newspaperCategory2.Id)
             .WithTitle("پرسپولیس در سوگ")
             .WithDesciption("امیری و ترابی مردند")
             .WithRate(5)
             .WithTheNewTags(tag1.Id)
             .WithView(10)
             .Build();
            DbContext.Save(theNew3);

            var newspaperCategory = await _sut.GetHighestNewsCount();

            newspaperCategory.Id.Should().Be(newspaperCategory1.Id);
            newspaperCategory.CategoryName.Should().Be(category.Title);
            newspaperCategory.NewspaperName.Should().Be(newspaper.Title);
        }
        [Fact]
        public async Task GetCategories_gets_all_newspaperCategories_by_newspaperId()
        {
            var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(category);
            var category2 = new CategoryBuilder().WithRate(20).WithTitle("سیاسی").Build();
            DbContext.Save(category2);
            var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(tag1);
            var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(author);
            var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(newspaper);
            var newspaperCategory1 = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory1);
            var newspaperCategory2 = new NewspaperCategoryBuilder(category2.Id, newspaper.Id).Build();
            DbContext.Save(newspaperCategory2);

            var actual =await _sut.GetCategories(newspaper.Id);

            actual.Count().Should().Be(2);
            var newsCategory= actual.FirstOrDefault(_=>_.Id== newspaperCategory1.Id);
            newsCategory.Id.Should().Be(newspaperCategory1.Id);
            newsCategory.CategoryTitle.Should().Be(category.Title);
            var newsCategory2 = actual.FirstOrDefault(_ => _.Id == newspaperCategory2.Id);
            newsCategory2.Id.Should().Be(newspaperCategory2.Id);
            newsCategory2.CategoryTitle.Should().Be(category2.Title);

        }
    }
}
