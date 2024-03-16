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
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Exceptions;
using NewspaperManangment.Services.TheNews.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Spec.Tests.Newspapers
{
    [Scenario("منتشر کردن روزنامه")]
    [Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "   روزنامه ای که در فهرست روزنامه ها وجود دارد را منتشر کنم  ",
    InOrderTo = "   کسب درامد کنم .")]
    public class FaildToPublishNewspaperDueToIncompeleteNews : BusinessIntegrationTest
    {
        private readonly NewspaperService _sut;
        private Newspaper _newspaper;
        private NewspaperCategory _newspaperCategory;
        private Category _category;
        private Tag _tag1;
        private Author _author;
        private TheNew _theNew1;
     private Func<Task>? _actual;
        public FaildToPublishNewspaperDueToIncompeleteNews()
        {
            _sut = NewspaperServiceFactory.Create(SetupContext);
        }
        [Given("یک روزنامه با عنوان طلوع و با دسته بندی ورزشی در فهرست روزنامه ها وجود دارد.  ")]
        [And("  یک دسته بندی با عنوان ورزشی و وزن 20  در فهرست دسته بندی ها وجود دارد   ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال در دسته بندی ورزشی وجود دارد ")]
        [And("  یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد.   ")]
        [And("  خبر با عنوان پرسپولیس در لیگ و محتوای پرسپولیس قهرمان لیگ شد و وزن 15 و تگ  فوتبال  از نویسنده با نام زهرااحمدی  را دردسته بندی ورزشی از روزنامه طلوع وجود دارد. ")]
        [And("  خبر دیگر با عنوان استقلال در سوگ و محتوای بازیکن استقلال مرد و وزن 5 و تگ  فوتبال از نویسنده با نام زهرااحمدی  دردسته بندی ورزشی از روزنامه طلوع وجود دارد. ")]
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
            _theNew1 = new TheNewBuilder(_author.Id, _newspaperCategory.Id)
              .WithTitle("پرسپولیس در لیگ")
              .WithDesciption("پرسپولیس قهرمان لیگ شد")
              .WithRate(15)
              .WithTheNewTags(_tag1.Id)
            .Build();
            DbContext.Save(_theNew1);
       
        }
        [When("من روزنامه را منتشر میکنم")]
        private void When()
        {

           _actual=()=> _sut.Publish(_newspaper.Id);

        }
        [Then("باید خطای عدم منتشر شدن روزنامه به علت ناقص بودن اخباردر دسته بندی روزنامه را مشاهده کنم")]
        private async Task Then()
        {
          await  _actual.Should().ThrowExactlyAsync<NewspaperIsNotCompeletedToBePublishedException>();

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
