using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangement.Test.Tools.TheNews;
using NewspaperManangment.Services.TheNews.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
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

           var theNew=await _sut.GetToIncreaseView(theNew1.Id);

            theNew.Id.Should().Be(theNew1.Id);
            theNew.Title .Should().Be(theNew1.Title);
            theNew.Description.Should().Be(theNew1.Description);
            theNew.AuthorName.Should().Be(theNew1.Author.FullName);
            theNew.Rate.Should().Be(theNew1.Rate);

        }
    }
}
