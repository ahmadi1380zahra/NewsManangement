using Azure;
using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
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
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Spec.Tests.TheNews
{
    [Scenario("حذف کردن خبر")]
    [Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "   خبری را از دسته بندی  روزنامه حذف کنم  ",
    InOrderTo = "  خبرهای معتبر منتشر کنم")]

    public class TheNewDeleteTest : BusinessIntegrationTest
    {
        private readonly TheNewService _sut;
        private Newspaper _newspaper;
        private NewspaperCategory _newspaperCategory;
        private Category _category;
        private Tag _tag1;
        private Author _author;
        private TheNew _theNew;
        public TheNewDeleteTest()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Given("یک روزنامه با عنوان طلوع و با دسته بندی ورزشی در فهرست روزنامه ها وجود دارد.  ")]
        [And("  یک دسته بندی با عنوان ورزشی و وزن 20  در فهرست دسته بندی ها وجود دارد   ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال در دسته بندی ورزشی وجود دارد ")]
        [And("  یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد.   ")]
        [And("  خبر با عنوان پرسپولیس در لیگ و محتوای پرسپولیس قهرمان لیگ شد و وزن 15 و تگ  فوتبال  از نویسنده با نام زهرااحمدی  را دردسته بندی ورزشی از روزنامه طلوع وجود دارد. ")]
        private void Given()
        {
            _category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(_category);
            _tag1 = new TagBuilder(_category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(_tag1);
            _author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(_author);
            _newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(_newspaper);
            _newspaperCategory = new NewspaperCategoryBuilder(_category.Id, _newspaper.Id).Build();
            DbContext.Save(_newspaperCategory);
            _theNew = new TheNewBuilder(_author.Id, _newspaperCategory.Id)
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(15)
              .WithTheNewTags(_tag1.Id)
            .Build();
            DbContext.Save(_theNew);
        }

        [When("من خبر با عنوان پرسپولیس در لیگ را از دسته بندی خبر های ورزشی روزنامه طلوع حذف می کنم")]
        private async Task When()
        {

            await _sut.Delete(_theNew.Id);

        }

        [Then(" باید در فهرست دسته بندی خبر های روزنامه طلوع هیچ خبری وجود نداشته باشد.")]
        private void Then()
        {
            var news = ReadContext.TheNews.FirstOrDefault(_=>_.Id== _theNew.Id);
            news.Should().BeNull();
            var tags = ReadContext.TheNewTags.Any(_ => _.TheNewId == _theNew.Id);
            tags.Should().BeFalse();
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
