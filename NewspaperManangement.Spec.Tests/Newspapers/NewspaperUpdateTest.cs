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

[Scenario("ویرایش کردن عنوان روزنامه")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "  روزنامه ای که در فهرست روزنامه ها وجود دارد را ویرایش کنم  ",
    InOrderTo = "  فقط روزنامه معتبر درفهرست روزنامه ها داشته باشم .")]
public class NewspaperUpdateTest : BusinessIntegrationTest
{
    private readonly NewspaperService _sut;
    private Category _category1;
    private Newspaper _newspaper;
    public NewspaperUpdateTest()
    {
        _sut = NewspaperServiceFactory.Create(SetupContext);
    }

    [Given("  یک دسته بندی با عنوان جنایی در فهرست دسته بندی ها وجود دارد ")]
    [And("  یک روزنامه با عنوان طلوع و با دسته بندی جنایی در فهرست روزنامه ها وجود دارد ")]
    private void Given()
    {
        _category1 = new CategoryBuilder().WithTitle("جنایی").Build();
        DbContext.Save(_category1);
        _newspaper = new NewspaperBuilder().WithCategory(_category1.Id).Build();
        DbContext.Save(_newspaper);

    }

    [When("من روزنامه با عنوان طلوع  به روزنامه با عنوان انقلاب تغییر میدهم")]
    private async Task When()
    {
        var dto = new UpdateNewsPaperDto
        {
            Title = "انقلاب",

        };
        await _sut.Update(_newspaper.Id, dto);

    }

    [Then("باید در فهرست روزنامه ها فقط یک روزنامه با عنوان  انقلاب  که دسته بندی ذکر شده را دارد وجود داشته  باشد.")]
    private void Then()
    {
        var actual = ReadContext.Newspapers.FirstOrDefault(_ => _.Id == _newspaper.Id);
        actual.Title.Should().Be("انقلاب");
        var newspaperCategory = ReadContext.NewspaperCategories.FirstOrDefault(_ => _.NewspaperId == _newspaper.Id);
        newspaperCategory.CategoryId.Should().Be(_category1.Id);

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