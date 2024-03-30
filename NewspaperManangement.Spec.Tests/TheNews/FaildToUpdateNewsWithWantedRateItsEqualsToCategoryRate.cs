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
using NewspaperManangment.Services.TheNews.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Spec.Tests.TheNews
{
    [Scenario("عدم ویرایش شدن خبر به علت علت  برابر بودن وزن خبر با وزن دسته بندی")]
    [Story("",
  AsA = "مدیر روزنامه  ",
  IWantTo = "   خبر ویرایش کنم  ",
  InOrderTo = "   خبرهای معتبری را منتشر کنم.")]
    public class FaildToUpdateNewsWithWantedRateItsEqualsToCategoryRate : BusinessIntegrationTest
    {
        private readonly TheNewService _sut;
        private Newspaper _newspaper;
        private NewspaperCategory _newspaperCategory;
        private Category _category;
        private Tag _tag1;
        private Author _author;
        private TheNew _theNew;
        private Func<Task>? _actual;
        public FaildToUpdateNewsWithWantedRateItsEqualsToCategoryRate()
        {
            _sut = TheNewServiceFactory.Create(SetupContext);
        }
        [Given("یک روزنامه با عنوان طلوع و با دسته بندی ورزشی در فهرست روزنامه ها وجود دارد.  ")]
        [And("  یک دسته بندی با عنوان ورزشی و وزن 20  در فهرست دسته بندی ها وجود دارد   ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال در دسته بندی ورزشی وجود دارد ")]
        [And("  یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد.   ")]
        [And("  خبر با عنوان پرسپولیس در اسیا و محتوای پرسپولیس قهرمان اسیا شد و وزن 10 و تگ  فوتبال  از نویسنده با نام زهرااحمدی  را دردسته بندی ورزشی از روزنامه طلوع وجود دارد. ")]

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
            .WithTitle("پرسپولیس در اسیا")
            .WithDesciption("پرسپولیس قهرمان اسیا شد")
            .WithRate(10)
            .Build();
            DbContext.Save(_theNew);
        }

        [When("من خبر با عنوان پرسپولیس در اسیا و محتوای پرسپولیس قهرمان اسیا شد به وزن 20 و تگ  فوتبال از نویسنده با نام زهرااحمدی  را ازدسته بندی ورزشی از روزنامه طلوع ویرایش میکنم.")]
        private void When()
        {
            var dto = new UpdateTheNewDto
            {
                Title = "پرسپولیس در اسیا",
                Description = "پرسپولیس قهرمان اسیا شد",
                Rate = 20,
                AuthorId = _author.Id,
                NewsPaperCategoryId = _newspaperCategory.Id,

            };
            _actual = () => _sut.Update(_theNew.Id, dto);

        }

        [Then("باید خطای عدم ویرایش شدن خبر به علت  برابر بودن وزن خبر با وزن دسته بندی مشاهده کنم..")]
        private async Task Then()
        {
            await _actual.Should().ThrowExactlyAsync<TheNewsRateCantBeEqualToCategoryRateItShouldBeLessException>();
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
