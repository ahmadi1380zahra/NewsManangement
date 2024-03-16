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
using NewspaperManangment.Services.TheNews.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Spec.Tests.TheNews
{
    [Scenario("عدم ثبت شدن خبر به علت پر بودن وزن دسته بندی")]
    [Story("",
  AsA = "مدیر روزنامه  ",
  IWantTo = "  میخواهم خبر به دسته بندی روزنامه اضافه کنم  ",
  InOrderTo = "  بتوانم خبرها را منتشر کنم.")]
    public class FaildToAddNewsWithWantedRateItsMoreThanCategoryLimit : BusinessIntegrationTest
    {
        private readonly TheNewService _sut;
        private Newspaper _newspaper;
        private NewspaperCategory _newspaperCategory;
        private Category _category;
        private Tag _tag1;
        private Tag _tag2;
        private Author _author;
        private TheNew _theNew;
        private Func<Task>? _actual;
        public FaildToAddNewsWithWantedRateItsMoreThanCategoryLimit()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Given("یک روزنامه با عنوان طلوع و با دسته بندی ورزشی در فهرست روزنامه ها وجود دارد.  ")]
        [And("  یک دسته بندی با عنوان ورزشی و وزن 20  در فهرست دسته بندی ها وجود دارد   ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال در دسته بندی ورزشی وجود دارد ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال چمن در دسته بندی ورزشی وجود دارد ")]
        [And("  یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد.   ")]
        [And("  خبر با عنوان پرسپولیس در لیگ و محتوای پرسپولیس قهرمان لیگ شد و وزن 15 و تگ  فوتبال وفوتبال چمن از نویسنده با نام زهرااحمدی  را دردسته بندی ورزشی از روزنامه طلوع وجود دارد. ")]

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
            _theNew = new TheNewBuilder(_author.Id, _newspaperCategory.Id)
                .WithTitle("پرسپولیس در لیگ")
                .WithDesciption("پرسپولیس قهرمان لیگ شد")
                .WithRate(15)
                .WithTheNewTags(_tag1.Id)
                .WithTheNewTags(_tag2.Id)
                .Build();
            DbContext.Save(_theNew);
        }

        [When("من خبر با عنوان پرسپولیس در اسیا و محتوای پرسپولیس قهرمان اسیا شد و وزن 18 و تگ  فوتبال وفوتبال چمن از نویسنده با نام زهرااحمدی  را به دسته بندی ورزشی از روزنامه طلوع اضافه میکنم..")]
        private void  When()
        {
            var dto = new AddTheNewDto
            {
                Title = "پرسپولیس در اسیا",
                Description = "پرسپولیس قهرمان اسیا شد",
                Rate = 18,
                AuthorId = _author.Id,
                NewsPaperCategoryId = _newspaperCategory.Id,
                TagsId = new List<int>
                {
                    _tag1.Id,
                    _tag2.Id
                }
            };


           _actual=()=> _sut.Add(dto);

        }

        [Then("باید خطای عدم ثبت شدن خبر به علت پر بودن وزن دسته بندی مشاهده کنم")]
        private async Task Then()
        {
           await _actual.Should().ThrowExactlyAsync<TheNewspaperCategoryIsFullException>();
        }


        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When(),
                _ => Then().Wait());
        }

    }
}
