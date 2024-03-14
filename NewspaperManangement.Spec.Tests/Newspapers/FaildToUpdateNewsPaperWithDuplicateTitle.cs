using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Newspapers.Contracts;
using NewspaperManangment.Services.Newspapers.Contracts.Dtos;
using NewspaperManangment.Services.Newspapers.Exceptions;
using Xunit;

namespace NewspaperManangement.Spec.Tests.Newspapers;

[Scenario("عدم ویرایش شدن عنوان روزنامه به علت تکراری بودن عنوان")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "  روزنامه ای که در فهرست روزنامه ها وجود دارد را ویرایش کنم  ",
    InOrderTo = "  فقط روزنامه معتبر درفهرست روزنامه ها داشته باشم .")]
public class FaildToUpdateNewsPaperWithDuplicateTitle : BusinessIntegrationTest
{
    private readonly NewspaperService _sut;
  private Category _category1;
  private Newspaper _newspaper1;
    private Newspaper _newspaper2;
    private Func<Task>? _actual;
    public FaildToUpdateNewsPaperWithDuplicateTitle()
    {
        _sut = NewspaperServiceFactory.Create(SetupContext);
    }
    [Given("  یک روزنامه با عنوان طلوع و با دسته بندی جنایی در فهرست روزنامه ها وجود دارد. . ")]
    [And(" یک روزنامه با عنوان انقلاب و با دسته بندی جنایی در فهرست روزنامه ها وجود دارد. ")]
    [And("  یک دسته بندی دیگر با عنوان جنایی در فهرست دسته بندی ها وجود دارد. . ")]
    
    private void Given()
    {
        _category1 = new CategoryBuilder().WithTitle("جنایی").Build();
        DbContext.Save(_category1);
        _newspaper1 = new NewspaperBuilder()
            .WithTitle("طلوع")
            .WithCategory(_category1.Id).Build();
        DbContext.Save(_newspaper1);
        _newspaper2 = new NewspaperBuilder()
           .WithTitle("انقلاب")
           .WithCategory(_category1.Id).Build();
        DbContext.Save(_newspaper2);

    }

    [When("من روزنامه با عنوان طلوع  به روزنامه با عنوان انقلاب ویرایش می کنم.")]
    private void When()
    {
        var dto = new UpdateNewsPaperDto
        {
            Title = "انقلاب",

        };
        _actual =()=> _sut.Update(_newspaper1.Id, dto);

    }

    [Then("باید خطای عدم ویرایش شدن عنوان روزنامه به علت تکراری بودن عنوان را مشاهده کنم..")]
    private async Task Then()
    {
      await  _actual.Should().ThrowExactlyAsync<NewspaperShouldHaveUniqueNameException>();
    }


    [Fact]
    public void Run()
    {
        Runner.RunScenario(
               _ => Given(),
              _ => When(),
             _ => Then().Wait());
    }
}