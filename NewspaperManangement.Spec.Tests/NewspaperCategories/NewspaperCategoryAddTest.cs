using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using NewspaperManangment.Services.Newspapers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewspaperManangment.Services.NewspaperCategories.Contracts;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangment.Services.NewspaperCategories.Contracts.Dtos;

namespace NewspaperManangement.Spec.Tests.NewspaperCategories
{

    [Scenario("اضافه کردن دسته بندی به روزنامه")]
    [Story("",
        AsA = "مدیر روزنامه  ",
        IWantTo = " به روزنامه ای که در فهرست روزنامه ها وجود دارد دسته بندی اضافه کنم  ",
        InOrderTo = "  روزنامه دسته بندی های مناسبی داشته باشد.")]
    public class NewspaperCategoryAddTest : BusinessIntegrationTest
    {
        private readonly NewspaperCategoryService _sut;
        private Category _category1;
        private Category _category2;
        private Newspaper _newspaper;
        public NewspaperCategoryAddTest()
        {
            _sut = NewspaperCategoryServiceFactory.Create(SetupContext);
        }
        [Given("  یک دسته بندی با عنوان جنایی در فهرست دسته بندی ها وجود دارد ")]
        [And(" یک روزنامه با عنوان طلوع و با دسته بندی جنایی در فهرست روزنامه ها وجود دارد ")]
        [And(" یک دسته بندی با عنوان ورزشی در فهرست دسته بندی ها وجود دارد ")]
        private void Given()
        {
            _category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(_category1);
            _newspaper = new NewspaperBuilder().WithTitle("طلوع").WithCategory(_category1.Id).Build();
            DbContext.Save(_newspaper);
            _category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
            DbContext.Save(_category2);
        }

        [When("من به روزنامه با عنوان طلوع  یک دسته بندی جدید با عنوان ورزشی اضافه میکنم")]
        private async Task When()
        {
            var dto = new AddNewspaperCategoryDto
            {
                categoryId = _category2.Id,
                newspaperId = _newspaper.Id,
            };
            await _sut.Add(dto);

        }

        [Then("باید در فهرست روزنامه ها فقط یک روزنامه با عنوان طلوع که دسته بندی های ورزشی و جنایی را دارد وجود داشته  باشد..")]
        private void Then()
        {
            var newspaperCategory = ReadContext.NewspaperCategories
                .Any(_ => _.NewspaperId == _newspaper.Id && _.CategoryId==_category2.Id);

            newspaperCategory.Should().BeTrue();

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
