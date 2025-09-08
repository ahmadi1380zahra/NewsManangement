using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangment.Entities;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Tags;
using NewspaperManangment.Services.Application.Tags2;

namespace NewspaperManangement.Services.UnitTests.Tag2;

public class Tag2AddTest : BusinessIntegrationTest
{
    private readonly AddTagCommandHandler2 _sut;

    public Tag2AddTest()
    {
        var tagRepository = new EFTagRepository(SetupContext);
        var unit = new EFUnitOfWork(SetupContext);
        _sut = new AddTagCommandHandler2(tagRepository, unit);
    }

    [Fact]
    public async Task Add()
    {
        var category = new CategoryBuilder()
            .Build();
        Save(category);
        var dto = new AddTagCommand2
        {
            Title = "dummy",
            CategoryId = category.Id
        };

        var actual = await _sut.Handle(dto,new BasicCommand
        {
            UserId = "1",
            TenantId = "2"
        });

        var res =await ReadContext.Set<Tag>()
            .FirstOrDefaultAsync();
        res.CategoryId.Should().Be(category.Id);
        res.Title.Should().Be(dto.Title);
    } 
}