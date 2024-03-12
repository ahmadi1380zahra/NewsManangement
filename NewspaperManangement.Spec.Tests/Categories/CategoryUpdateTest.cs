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

[Scenario("ویرایش کردن دسته بندی")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = " دسته بندی که در فهرست دسته بندی ها وجود دارد را ویرایش کنم ",
    InOrderTo = "  دسته بندی های معتبری در فهرست دسته بندی ها داشته باشم")]
public class CategoryUpdateTest : BusinessIntegrationTest
{
    private readonly CategoryService _sut;
    private Category _category;
    public CategoryUpdateTest()
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

    [When("من دسته بندی مذکور را به دسته بندی با عنوان فرهنگی و وزن 15 ویرایش میکنم.")]
    private async Task When()
    {
        var dto = new UpdateCategoryDto
        {
            Title = "فرهنگی",
            Rate = 15
        };


        await _sut.Update(_category.Id,dto);

    }

    [Then("باید در فهرست دسته بندی ها فقط یک دسته بندی با عنوان فرهنگی و وزن 15 وجود داشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Categories.Single();
        actual.Title.Should().Be("فرهنگی");
        actual.Rate.Should().Be(15);
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