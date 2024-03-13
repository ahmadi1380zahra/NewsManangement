using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangment.Entities;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Categories;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using NewspaperManangment.Services.Catgories;
using NewspaperManangment.Services.Catgories.Contracts;
using NewspaperManangment.Services.Catgories.Contracts.Dtos;
using NewspaperManangment.Services.Tags.Contracts;
using NewspaperManangment.Services.Tags.Contracts.Dtos;
using Xunit;

namespace NewspaperManangement.Spec.Tests.Authors;

[Scenario("ثبت کردن نویسنده")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = " نویسنده به فهرست نویسنده ها اضافه کنم ",
    InOrderTo = "  خبر های این نویسنده ها را منتشر کنم.")]
public class AuthorAddTest : BusinessIntegrationTest
{
    private readonly AuthorService _sut;


    public AuthorAddTest()
    {
        _sut = AuthorServiceFactory.Create(SetupContext);
    }
    [Given(" هیچ نویسنده در فهرست  نویسنده ها وجود ندارد    ")]
    private void Given()
    {

    }

    [When("من نویسنده با عنوان زهرااحمدی  ثبت میکنم..")]
    private async Task When()
    {
        var dto = new AddAuthorDto
        {
            FullName = "زهرااحمدی",

        };
        await _sut.Add(dto);

    }

    [Then("باید در فهرست نویسنده ها فقط یک نویسنده با عنوان زهرااحمدی وجود داشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Authors.Single();
        actual.FullName.Should().Be("زهرااحمدی");
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