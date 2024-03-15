using Microsoft.EntityFrameworkCore;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using NewspaperManangment.Services.Tags.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using NewspaperManangment.Services.TheNews.Contracts;
using NewspaperManangement.Test.Tools.TheNews;
using FluentAssertions;

namespace NewspaperManangement.Spec.Tests.TheNews
{
    [Scenario("ثبت کردن خبر")]
    [Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "  میخواهم خبر به دسته بندی روزنامه اضافه کنم  ",
    InOrderTo = "  بتوانم خبرها را منتشر کنم.")]
    public class TheNewAddTest : BusinessIntegrationTest
    {
        private readonly TheNewService _sut;
        private Newspaper _newspaper;
        private NewspaperCategory _newspaperCategory;
        private Category _category;
        private Tag _tag1;
        private Tag _tag2;
        private Author _author;

        public TheNewAddTest()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Given("یک روزنامه با عنوان طلوع و با دسته بندی ورزشی در فهرست روزنامه ها وجود دارد.  ")]
        [And("  یک دسته بندی با عنوان ورزشی و وزن 20  در فهرست دسته بندی ها وجود دارد   ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال در دسته بندی ورزشی وجود دارد ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال چمن در دسته بندی ورزشی وجود دارد ")]
        [And("  یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد.   ")]

        private void Given()
        {
            _category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(_category);
            _tag1 = new TagBuilder(_category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(_tag1);
            _tag2 = new TagBuilder(_category.Id).WithTitle("فوتبال چمن").Build();
            DbContext.Save(_tag2);
            _author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(_author);
            _newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(_newspaper);
            _newspaperCategory = new NewspaperCategoryBuilder(_category.Id, _newspaper.Id).Build();
            DbContext.Save(_newspaperCategory);
        }

        [When("من خبر با عنوان پرسپولیس در لیگ و محتوای پرسپولیس قهرمان لیگ شد و وزن 10 و تگ  فوتبال از نویسنده با نام زهرااحمدی  را به دسته بندی ورزشی از روزنامه طلوع اضافه میکنم.")]
        private async Task When()
        {
            var dto = new AddTheNewDto
            {
                Title = "پرسپولیس در لیگ",
                Description = "پرسپولیس قهرمان لیگ شد",
                Rate = 10,
                AuthorId = _author.Id,
                NewsPaperCategoryId = _newspaperCategory.Id,
                TagsId = new List<int>
                {
                    _tag1.Id,
                    _tag2.Id
                }
            };


            await _sut.Add(dto);

        }

        [Then("باید در فهرست روزنامه ها فقط یک روزنامه با عنوان طلوع  که خبر مذکور را در دسته بندی ورزشی خود دارد ببینم.")]
        private void Then()
        {
            var newspaper = ReadContext.Newspapers.FirstOrDefault(_ => _.Id == _newspaper.Id);
            var news = ReadContext.TheNews.FirstOrDefault();
            var newstags = ReadContext.TheNewTags.Count(_ => _.TheNewId == news.Id);
            news.Title.Should().Be("پرسپولیس در لیگ");
            news.Description.Should().Be("پرسپولیس قهرمان لیگ شد");
            news.Rate.Should().Be(10);
            news.AuthorId.Should().Be(_author.Id);
            news.NewspaperCategoryId.Should().Be(_newspaperCategory.Id);
            newstags.Should().Be(2);
          
        }


        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then());
        }
    }
}
