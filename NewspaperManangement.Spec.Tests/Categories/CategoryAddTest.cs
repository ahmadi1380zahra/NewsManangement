using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Services.Catgories;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using Xunit;

namespace NewspaperManangement.Spec.Tests.Categories;

[Scenario("ثبت کردن دسته بندی")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "دسته بندی به فهرست دسته بندی ها اضافه کنم ",
    InOrderTo = "  روزنامه و خبر در این دسته بندی ها داشته باشم.")]
public class TagAddTest : BusinessIntegrationTest
{
    private readonly CategoryService _sut;
    

    public TagAddTest()
    {
        _sut = CategoryServiceFactory.Create(SetupContext);
    }
    [Given("هیچ دسته بندی در فهرست دسته بندی ها وجود ندارد  ")]

    private void Given()
    {

    }

    [When("من دسته بندی با عنوان جنایی و وزن 20 ثبت میکنم")]
    private async Task When()
    {
        var dto = new AddCategoryDto
        {
            Title = "جنایی",
            Rate = 20
        };


        await _sut.Add(dto);

    }

    [Then("باید در فهرست دسته بندی ها فقط یک دسته بندی با عنوان جنایی و وزن 20 وجود داشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Categories.Single();
        actual.Title.Should().Be("جنایی");
        actual.Rate.Should().Be(20);
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