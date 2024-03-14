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

[Scenario("عدم ثبت شدن روزنامه با نام تکراری")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = " روزنامه به فهرست روزنامه ها اضافه کنم  ",
    InOrderTo = "  خبر در این روزنامه منتشر کنم.")]
public class FaildToAddNewsPaperWithDuplicateTitle : BusinessIntegrationTest
{
    private readonly NewspaperService _sut;
    private AddNewsPaperDto _dto;
    private Category _category1;
    private Category _category2;
    private Category _category3;
    private Newspaper _newspaper;
    private Func<Task>? _actual;
    public FaildToAddNewsPaperWithDuplicateTitle()
    {
        _sut = NewspaperServiceFactory.Create(SetupContext);
    }
    [Given(" یک روزنامه با عنوان طلوع و با دسته بندی جنایی در فهرست روزنامه ها وجود دارد. . ")]
    [And("  یک دسته بندی با عنوان جنایی در فهرست دسته بندی ها وجود دارد. . ")]
    [And("  یک دسته بندی دیگر با عنوان ورزشی در فهرست دسته بندی ها وجود دارد. . ")]
    [And("  یک دسته بندی دیگر با عنوان فرهنگی در فهرست دسته بندی ها وجود دارد. . ")]
    private void Given()
    {
        _category1 = new CategoryBuilder().WithTitle("جنایی").Build();
        DbContext.Save(_category1);
        _newspaper = new NewspaperBuilder()
            .WithTitle("طلوع")
            .WithCategory(_category1.Id).Build();
        DbContext.Save(_newspaper);
        _category2 = new CategoryBuilder().WithTitle("ورزشی").Build();
        DbContext.Save(_category2);
        _category3 = new CategoryBuilder().WithTitle("فرهنگی").Build();
        DbContext.Save(_category3);
    }

    [When("من روزنامه با عنوان طلوع  با سه دسته بندی ذکر شده  ثبت میکنم.")]
    private void When()
    {
        _dto = new AddNewsPaperDto
        {
            Title = "طلوع",
            CategoriesId = new List<int>
              {
             _category1.Id,
             _category2.Id,
             _category3.Id
              }
        };
        _actual=()=> _sut.Add(_dto);

    }

    [Then("باید خطای عدم ثبت شدن روزنامه با نام تکراری را ببینم.")]
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