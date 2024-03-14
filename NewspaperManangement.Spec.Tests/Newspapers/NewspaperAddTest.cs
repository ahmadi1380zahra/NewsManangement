using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using Xunit;

namespace NewspaperManangement.Spec.Tests.Newspapers;

[Scenario("ثبت کردن روزنامه")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = " روزنامه به فهرست روزنامه ها اضافه کنم  ",
    InOrderTo = "  خبر در این روزنامه منتشر کنم.")]
public class NewspaperAddTest : BusinessIntegrationTest
{
    private readonly NewspaperService _sut;
    private AddNewsPaperDto _dto;
    private Category _category1;
    private Category _category2;
    private Category _category3;
    public NewspaperAddTest()
    {
        _sut = NewspaperServiceFactory.Create(SetupContext);
    }
    [Given(" هیچ روزنامه ای در فهرست  روزنامه ها وجود ندارد . ")]
    [And("  یک دسته بندی با عنوان جنایی در فهرست دسته بندی ها وجود دارد. . ")]
    [And("  یک دسته بندی دیگر با عنوان ورزشی در فهرست دسته بندی ها وجود دارد. . ")]
    [And("  یک دسته بندی دیگر با عنوان فرهنگی در فهرست دسته بندی ها وجود دارد. . ")]
    private void Given()
    {
        _category1 = new CategoryBuilder().WithTitle("جنایی").Build();
        DbContext.Save(_category1);
        _category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
        DbContext.Save(_category2);
        _category3 = new CategoryBuilder().WithTitle("فرهنگی").Build();
        DbContext.Save(_category3);
    }

    [When("من روزنامه با عنوان طلوع  با سه دسته بندی ذکر شده  ثبت میکنم.")]
    private async Task When()
    {
        _dto = new AddNewsPaperDto
        {
            Title = "طلوع",
            CategoriesId = new List<int>
              {
             _category1.Id,
             _category2.Id,
             _category3.Id
              }
        };
        await _sut.Add(_dto);

    }

    [Then("باید در فهرست روزنامه ها فقط یک روزنامه با عنوان طلوع  که سه دسته بندی ذکر شده را دارد  وجود داشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Newspapers.FirstOrDefault();
        var newspaperCategory = ReadContext.NewspaperCategories.FirstOrDefault();
        actual.Title.Should().Be(_dto.Title);
        newspaperCategory.CategoryId.Should().Be(_category1.Id);
        newspaperCategory.NewspaperId.Should().Be(actual.Id);
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