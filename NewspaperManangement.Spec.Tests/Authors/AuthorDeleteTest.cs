using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Authors.Contracts;
using NewspaperManangment.Services.Authors.Contracts.Dtos;
using Xunit;

namespace NewspaperManangement.Spec.Tests.Authors;

[Scenario("حذف کردن نویسنده")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "   نویسنده ی که در فهرست نویسنده ها وجود دارد را حذف کنم ",
    InOrderTo = "   فقط  نویسنده های معتبر را  در فهرست نویسنده ها داشته باشم")]
public class AuthorDeleteTest : BusinessIntegrationTest
{
    private readonly AuthorService _sut;
    private Author _author;

    public AuthorDeleteTest()
    {
        _sut = AuthorServiceFactory.Create(SetupContext);
    }
    [Given(" فقط یک نویسنده  با  نام زهرااحمدی  در فهرست نویسنده ها وجود دارد    ")]
    private void Given()
    {
        _author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
        DbContext.Save(_author);
    }

    [When("من نویسنده  مذکور را حذف میکنم.")]
    private async Task When()
    {
       
        await _sut.Delete(_author.Id);

    }

    [Then("باید در فهرست نویسنده  ها هیچ نویسنده ای  وجود نداشته باشد..")]
    private void Then()
    {
        var actual = ReadContext.Authors.FirstOrDefault(_ => _.Id == _author.Id);
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