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
    [Scenario("ویرایش کردن خبر")]
    [Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "   خبری را از دسته بندی  روزنامه ویرایش  کنم  ",
    InOrderTo = "  خبرهای معتبر منتشر کنم")]

    public class TheNewUpdateTest : BusinessIntegrationTest
    {
        private readonly TheNewService _sut;
        private Newspaper _newspaper;
        private NewspaperCategory _newspaperCategory;
        private Category _category;
        private Tag _tag1;
        private Author _author;
        private Author _author2;
        private TheNew _theNew;
        public TheNewUpdateTest()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Given("یک روزنامه با عنوان طلوع و با دسته بندی ورزشی در فهرست روزنامه ها وجود دارد.  ")]
        [And("  یک دسته بندی با عنوان ورزشی و وزن 20  در فهرست دسته بندی ها وجود دارد   ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال در دسته بندی ورزشی وجود دارد ")]
        [And("  یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد.   ")]
        [And("  یک نویسنده  با  نام علی رضااحمدی  در فهرست نویسنده ها وجود دارد.   ")]
        [And("  خبر با عنوان پرسپولیس در لیگ و محتوای پرسپولیس قهرمان لیگ شد و وزن 10 و تگ  فوتبال  از نویسنده با نام زهرااحمدی  را دردسته بندی ورزشی از روزنامه طلوع وجود دارد. ")]
        private void Given()
        {
            _category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
            DbContext.Save(_category);
            _tag1 = new TagBuilder(_category.Id).WithTitle("فوتبال").Build();
            DbContext.Save(_tag1);
            _author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
            DbContext.Save(_author);
            _author2 = new AuthorBuilder().WithFullName("علی رضا احمدی").Build();
            DbContext.Save(_author2);
            _newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
            DbContext.Save(_newspaper);
            _newspaperCategory = new NewspaperCategoryBuilder(_category.Id, _newspaper.Id).Build();
            DbContext.Save(_newspaperCategory);
            _theNew = new TheNewBuilder(_author.Id, _newspaperCategory.Id)
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(10)
              .WithTheNewTags(_tag1.Id)
            .Build();
            DbContext.Save(_theNew);
        }

        [When("من خبر با عنوان پرسپولیس در لیگ به پرسپولیس در اسیاو محتوای پرسپولیس قهرمان اسیا شد و وزن 15 و تگ  فوتبال از نویسنده با نام علی رضااحمدی  را به دسته بندی ورزشی از روزنامه طلوع ویرایش میکنم.")]
        private async Task When()
        {
            var dto = new UpdateTheNewDto
            {
                Title = "پرسپولیس در اسیا",
                Description = "پرسپولیس قهرمان اسیا شد",
                Rate = 15,
                AuthorId = _author2.Id,
                NewsPaperCategoryId = _newspaperCategory.Id,
            };

            await _sut.Update(_theNew.Id, dto);
        }

        [Then(" باید در فهرست روزنامه ها فقط یک روزنامه با عنوان طلوع  که خبر با عنوان پرسپولیس در اسیاو محتوای پرسپولیس قهرمان اسیا شدو وزن 15 از نویسنده با نام علی رضااحمدی  را ببینم.")]
        private void Then()
        {
            var news = ReadContext.TheNews.FirstOrDefault(_ => _.Id == _theNew.Id);
            news.Title.Should().Be("پرسپولیس در اسیا");
            news.Description.Should().Be("پرسپولیس قهرمان اسیا شد");
            news.Rate.Should().Be(15);
            news.AuthorId.Should().Be(_author2.Id);
            news.NewspaperCategoryId.Should().Be(_newspaperCategory.Id);
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
