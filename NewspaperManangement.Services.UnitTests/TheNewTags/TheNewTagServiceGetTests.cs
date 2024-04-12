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
using NewspaperManangment.Services.TheNewTags.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Services.UnitTests.TheNewTags
{
    public class TheNewTagServiceGetTests:BusinessUnitTest
    {
        private readonly TheNewTagService _sut;
        public TheNewTagServiceGetTests()
        {
            _sut = TheNewTagServiceFactory.Create(SetupContext);
        }
       
        [Fact]
        public async Task GetTags_get_all_news_tags_by_newsId()
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
     .Build();
            DbContext.Save(theNew);
            var theNewTag = new TheNewTagBuilder(theNew.Id, tag1.Id).Build();
            DbContext.Save(theNewTag);
            var theNewTag2 = new TheNewTagBuilder(theNew.Id, tag2.Id).Build();
            DbContext.Save(theNewTag2);

            var actual =await _sut.GetTags(theNew.Id);

            actual.Count.Should().Be(2);
            var firstTheNewTag = actual.FirstOrDefault(_ => _.Id == theNewTag.Id);
            firstTheNewTag.Id.Should().Be(theNewTag.Id);
            firstTheNewTag.TagTitle.Should().Be(tag1.Title);
            var secondTheNewTag = actual.FirstOrDefault(_ => _.Id == theNewTag2.Id);
            secondTheNewTag.Id.Should().Be(theNewTag2.Id);
            secondTheNewTag.TagTitle.Should().Be(tag2.Title);
        }
    }
}
