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

[Scenario("حذف کردن روزنامه")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "  روزنامه ای که در فهرست روزنامه ها وجود دارد را حذف کنم  ",
    InOrderTo = "  فقط روزنامه معتبر درفهرست روزنامه ها داشته باشم .")]
public class NewspaperDeleteTest : BusinessIntegrationTest
{
    private readonly NewspaperService _sut;
    private Category _category1;
    private Newspaper _newspaper;
    public NewspaperDeleteTest()
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

    [When("من روزنامه مذکور را حذف میکنم.")]
    private async Task When()
    {

        await _sut.Delete(_newspaper.Id);

    }

    [Then("باید در فهرست روزنامه ها هیچ روزنامه ای  وجود نداشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Newspapers.FirstOrDefault(_ => _.Id == _newspaper.Id);
        var isAnyCategoryForNewspaper = ReadContext.NewspaperCategories
            .Any(_ => _.NewspaperId == _newspaper.Id);
        actual.Should().BeNull();
        isAnyCategoryForNewspaper.Should().BeFalse();

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