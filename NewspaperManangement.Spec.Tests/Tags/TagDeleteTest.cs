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

[Scenario("حذف کردن  تگ")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = " تگی که در فهرست  تگ ها وجود دارد را حذف کنم ",
    InOrderTo = "  فقط تگ های معتبر را  در فهرست تگ ها داشته باشم.")]
public class TagDeleteTest : BusinessIntegrationTest
{
    private readonly TagService _sut;
    private Category _category;
    private Tag _tag;

    public TagDeleteTest()
    {
        _sut = TagServiceFactory.Create(SetupContext);
    }
    [Given("در فهرست تگ ها فقط یک تگ با عنوان سرقت در دسته بندی جنایی وجود دارد.   ")]
    [And(" یک دسته بندی با عنوان جنایی در فهرست دسته بندی ها وجود دارد.   ")]

    private void Given()
    {
        _category = new CategoryBuilder().WithTitle("جنایی").Build();
        DbContext.Save(_category);
        _tag = new TagBuilder(_category.Id).WithTitle("سرقت").Build();
        DbContext.Save(_tag);
    }

    [When("من تگ مذکور  را حذف میکنم.")]
    private async Task When()
    {
        

        await _sut.Delete(_tag.Id);

    }

    [Then("باید در فهرست تگ ها هیچ تگی وجود نداشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Tags.FirstOrDefault(_=>_.Id==_tag.Id);
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