using FluentAssertions;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using Xunit;

namespace NewspaperManangement.Spec.Tests.Authors;

[Scenario("ویرایش کردن نویسنده")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = " نویسنده ی که در فهرست نویسنده ها وجود دارد را ویرایش کنم ",
    InOrderTo = "  نویسنده های معتبری در فهرست نویسنده ها داشته باشم.")]
public class AuthorUpdateTest : BusinessIntegrationTest
{
    private readonly AuthorService _sut;
    private Author _author;

    public AuthorUpdateTest()
    {
        _sut = AuthorServiceFactory.Create(SetupContext);
    }
    [Given(" فقط یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد    ")]
    private void Given()
    {
        _author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
        DbContext.Save(_author);
    }

    [When("من نویسنده  مذکور را به نویسنده  به نام نیلوفر حقیقت  ویرایش میکنم.")]
    private async Task When()
    {
        var dto = new UpdateAuthorDto
        {
            FullName = "نیلوفر حقیقت",

        };
        await _sut.Update(_author.Id,dto);

    }

    [Then("باید در فهرست نویسنده ها فقط یک نویسنده با  با نام نیلوفر حقیقت  وجود داشته باشد.")]
    private void Then()
    {
        var actual = ReadContext.Authors.Single();
        actual.FullName.Should().Be("نیلوفر حقیقت");
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