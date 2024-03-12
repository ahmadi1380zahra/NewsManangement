using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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

[Scenario("ویرایش کردن تگ")]
[Story("",
    AsA = "مدیر روزنامه  ",
    IWantTo = "تگی که در فهرست  تگ  ها وجود دارد را ویرایش کنم  ",
    InOrderTo = "  تگ  های معتبری در فهرست تگ  ها داشته باشم.")]
public class TagUpdateTest : BusinessIntegrationTest
{
    private readonly TagService _sut;
    private Category _category1;
    private Category _category2;
    private Tag _tag;

    public TagUpdateTest()
    {
        _sut = TagServiceFactory.Create(SetupContext);
    }
    [Given("یک دسته بندی با عنوان  جنایی در  دسته بندی ها وجود دارد.   ")]
    [And(" یک دسته بندی با عنوان فرهنگی در فهرست دسته بندی ها وجود دارد.   ")]
    [And("  در فهرست تگ ها فقط یک تگ با عنوان ادبیات معاصر در دسته بندی جنایی وجود دارد.   ")]

    private void Given()
    {
        _category1 = new CategoryBuilder().WithTitle("جنایی").Build();
        DbContext.Save(_category1);
        _category2 = new CategoryBuilder().WithTitle("فرهنگی").Build();
        DbContext.Save(_category2);
        _tag = new TagBuilder(_category1.Id).WithTitle("ادبیات معاصر").Build();
        DbContext.Save(_tag);
    }

    [When("من تگ مذکور را با عنوان ادبیات فرهنگی به دسته بندی با عنوان فرهنگی  ویرایش میکنم.")]
    private async Task When()
    {
        var dto = new UpdateTagDto
        {
            Title = "ادبیات فرهنگی",
            CategoryId = _category2.Id
        };


        await _sut.Update(_tag.Id,dto);

    }

    [Then(" باید در فهرست تگ ها فقط یک تگ با عنوان ادبیات فرهنگی در دسته بندی با عنوان فرهنگی  وجود داشته باشد")]
    private void Then()
    {
        var actual = ReadContext.Tags.Single();
        actual.Title.Should().Be("ادبیات فرهنگی");
        actual.CategoryId.Should().Be(_category2.Id);
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