using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangement.Test.Tools.TheNews;
using NewspaperManangement.Test.Tools.TheNewTags;
using NewspaperManangment.Entities;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.TheNewTags;
using NewspaperManangment.Services.TheNews.Contracts.Dtos;
using NewspaperManangment.Services.TheNewTags;
using NewspaperManangment.Services.TheNewTags.Contracts;
using NewspaperManangment.Services.TheNewTags.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperManangement.Spec.Tests.TheNewTags
{
    [Scenario("اضافه کردن تگ به خبر")]
    [Story("",
AsA = "مدیر روزنامه  ",
IWantTo = "  تگ به خبرهای دسته بندی یک روزنامه اضافه کنم  ",
InOrderTo = "  خبر تگ های مناسبی داشته باشد")]
    public class TheNewTagAddTest : BusinessIntegrationTest
    {
        private readonly TheNewTagService _sut;
        private Newspaper _newspaper;
        private NewspaperCategory _newspaperCategory;
        private Category _category;
        private Tag _tag1;
        private Tag _tag2;
        private Author _author;
        private TheNew _theNew;
        public TheNewTagAddTest()
        {
            _sut = TheNewTagServiceFactory.Create(SetupContext);
        }
        [Given("یک روزنامه با عنوان طلوع و با دسته بندی ورزشی در فهرست روزنامه ها وجود دارد.  ")]
        [And("  یک دسته بندی با عنوان ورزشی و وزن 20  در فهرست دسته بندی ها وجود دارد   ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال در دسته بندی ورزشی وجود دارد ")]
        [And("در فهرست تگ ها  یک تگ با عنوان فوتبال چمن در دسته بندی ورزشی وجود دارد ")]
        [And("  یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد.   ")]
        [And("  خبر با عنوان پرسپولیس در اسیا و محتوای پرسپولیس قهرمان اسیا شد و وزن 10 و تگ  فوتبال  از نویسنده با نام زهرااحمدی  را دردسته بندی ورزشی از روزنامه طلوع وجود دارد. ")]

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
         .WithTitle("پرسپولیس در اسیا")
         .WithDesciption("پرسپولیس قهرمان اسیا شد")
         .WithRate(10)
         .WithTheNewTags(_tag1.Id)
       .Build();
            DbContext.Save(_theNew);
        }
        [When("من به خبر با عنوان پرسپولیس در اسیا  از دسته بندی ورزشی روزنامه با عنوان طلوع  یک تگ جدید با عنوان فوتبال چمن اضافه میکنم.")]
        private async Task When()
        {
            var dto = new AddTheNewTagDto
            {
                TheNewId = _theNew.Id,
                TagId = _tag2.Id
            };

            await _sut.Add(dto);
        }
        [Then("باید در فهرست خبرهای روزنامه طلوع خبر با عنوان پرسپولیس در اسیا دارای دو تگ فوتبال و فوتبال چمن باشد.")]
        private void Then()
        {
            var theNewTags = ReadContext.TheNewTags.Where(_ => _.TheNewId == _theNew.Id);
            theNewTags.Count().Should().Be(2);
            theNewTags.Any(_ => _.TagId == _tag1.Id).Should().BeTrue();
            theNewTags.Any(_ => _.TagId == _tag2.Id).Should().BeTrue(); 

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
