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

    [Scenario("حذف کردن دسته بندی از روزنامه")]
    [Story("",
        AsA = "مدیر روزنامه  ",
        IWantTo = " دسته بندی های روزنامه را حذف کنم  ",
        InOrderTo = "  روزنامه فقط  دسته بندی های معتبر داشته باشد.")]
    public class NewspaperCategoryDeleteTest : BusinessIntegrationTest
    {
        private readonly NewspaperCategoryService _sut;
        private Category _category1;
        private Category _category2;
        private Newspaper _newspaper;
        private NewspaperCategory _newspaperCategory;
        public NewspaperCategoryDeleteTest()
        {
            _sut = NewspaperCategoryServiceFactory.Create(SetupContext);
        }
        [Given("  یک دسته بندی با عنوان جنایی در فهرست دسته بندی ها وجود دارد ")]
        [And("  یک دسته بندی با عنوان ورزشی در فهرست دسته بندی ها وجود دارد ")]
        [And(" یک روزنامه با عنوان طلوع و با دسته بندی جنایی و ورزشی در فهرست روزنامه ها وجود دارد ")]
        private void Given()
        {
            _category1 = new CategoryBuilder().WithTitle("جنایی").Build();
            DbContext.Save(_category1);
            _category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
            DbContext.Save(_category2);
            _newspaper = new NewspaperBuilder().WithTitle("طلوع").WithCategory(_category2.Id).Build();
            DbContext.Save(_newspaper);
            _newspaperCategory = new NewspaperCategoryBuilder(_category1.Id, _newspaper.Id)
                .Build();
            DbContext.Save(_newspaperCategory);
           
        }

        [When("من دسته بندی جنایی را از دسته بندی های روزنامه با عنوان طلوع  حذف میکنم.")]
        private async Task When()
        {
          
            await _sut.Delete(_newspaperCategory.Id);

        }

        [Then("باید در فهرست روزنامه ها فقط یک روزنامه با عنوان طلوع که فقط دسته بندی های ورزشی  را دارد وجود داشته  باشد..")]
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
