using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangment.Entities;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Services.Catgories;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using Xunit;

namespace NewspaperManangement.Spec.Tests.Tags;

[Scenario("ثبت کردن تگ")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "تگ به فهرست تگ ها اضافه کنم ",
    InOrderTo = "  خبر با این تگ ها داشته باشم.")]
public class TagAddTest : BusinessIntegrationTest
{
    private readonly TagService _sut;
    private Category _category;

    public TagAddTest()
    {
        _sut = TagServiceFactory.Create(SetupContext);
    }
    [Given("هیچ تگی در فهرست  تگ ها وجود ندارد   ")]
    [And(" یک دسته بندی با عنوان جنایی در فهرست دسته بندی ها وجود دارد.   ")]

    private void Given()
    {
        _category = new CategoryBuilder().WithTitle("جنایی").Build();
        DbContext.Save(_category);
    }

    [When("من تگ با عنوان قتل در دسته بندی جنایی ثبت میکنم.")]
    private async Task When()
    {
        var dto = new AddTagDto
        {
            Title = "قتل",
            CategoryId = _category.Id
        };


        await _sut.Add(dto);

    }

    [Then("باید در فهرست تگ ها فقط یک تگ با عنوان قتل  در دسته بندی  جنایی وجود داشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Tags.Single();
        actual.Title.Should().Be("قتل");
        actual.CategoryId.Should().Be(_category.Id);
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