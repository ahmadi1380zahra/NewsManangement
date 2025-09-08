using FluentAssertions;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangment.Persistance.EF.Mediate.Tag4;

namespace NewspaperManangement.Services.UnitTests.Tag2;

public class GetTest: BusinessIntegrationTest
{
    private readonly EFGetTag _sut;
    
    public GetTest()
    {
        _sut = new EFGetTag(SetupContext);
    }

    [Fact]
    public async Task get_by()
    {
        var category = new CategoryBuilder()
            .Build();
        Save(category);
        var tag = new TagBuilder(category.Id)
            .WithTitle("zaza")
            .Build();
        Save(tag);

        var res = await _sut.Handle(new GetTagDtoById
        {
            Id = tag.Id,
            
        });
        res.Title.Should().Be(tag.Title);
    }
}