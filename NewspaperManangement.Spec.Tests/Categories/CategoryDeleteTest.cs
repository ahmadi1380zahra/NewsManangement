using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangment.Entities;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Services.Catgories;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using Xunit;

namespace NewspaperManangement.Spec.Tests.Categories;

[Scenario("حذف کردن دسته بندی")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "  دسته بندی که در فهرست دسته بندی ها وجود دارد را حذف کنم ",
    InOrderTo = "  فقط  دسته بندی های معتبر را  در فهرست دسته بندی ها داشته باشم")]
public class CategoryDeleteTest : BusinessIntegrationTest
{
    private readonly CategoryService _sut;
    private Category _category;
    public CategoryDeleteTest()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }
    [Given("فقط یک دسته بندی با عنوان جنایی و وزن 20 در فهرست دسته بندی ها وجود دارد.  ")]

    private void Given()
    {
        _category = new CategoryBuilder()
            .WithTitle("جنایی")
            .WithRate(20)
            .Build();
        DbContext.Save(_category);
    }

    [When("من دسته بندی مذکور را حذف میکنم.")]
    private async Task When()
    {
       
        await _sut.Delete(_category.Id);

    }

    [Then("باید در فهرست دسته بندی ها هیچ دسته بندی وجود نداشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Categories.FirstOrDefault(_ => _.Id == _category.Id);
        actual.Should().BeNull();
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