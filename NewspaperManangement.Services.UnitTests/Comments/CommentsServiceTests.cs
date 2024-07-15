using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NewspaperManangement.Test.Tools.Authors;
using NewspaperManangement.Test.Tools.Categories;
using NewspaperManangement.Test.Tools.Comments;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig;
using NewspaperManangement.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using NewspaperManangement.Test.Tools.NewspaperCategories;
using NewspaperManangement.Test.Tools.Newspapers;
using NewspaperManangement.Test.Tools.Tags;
using NewspaperManangement.Test.Tools.TheNews;
using NewspaperManangment.Entities;
using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Comments;
using NewspaperManangment.Services.Comments;
using NewspaperManangment.Services.Comments.Contracts;
using NewspaperManangment.Services.Comments.Contracts.Dtos;

namespace NewspaperManangement.Services.UnitTests.Comments;

public class CommentsServiceTests : BusinessUnitTest
{
    private readonly CommentService _sut;

    public CommentsServiceTests()
    {
        _sut = CommentServiceFactory.Create(SetupContext);
    }

    [Fact]
    public async Task add_adds_a_comment_properly()
    {
        var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
        DbContext.Save(category);
        var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
        DbContext.Save(tag1);
        var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
        DbContext.Save(author);
        var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
        DbContext.Save(newspaper);
        var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
        DbContext.Save(newspaperCategory);
        var theNew = new TheNewBuilder(author.Id, newspaperCategory.Id)
            .WithTitle("پرسپولیس در لیگ")
            .WithDesciption("پرسپولیس قهرمان لیگ شد")
            .WithRate(10)
            .WithTheNewTags(tag1.Id)
            .Build();
        DbContext.Save(theNew);
        var dto = new AddCommentDtoBuilder()
            .WithTheNewId(theNew.Id)
            .WithComment("dummy")
            .Build();
        
        await _sut.Add(dto);

        var actual = await ReadContext.Set<Comment>().FirstOrDefaultAsync();
        actual.comment.Should().Be(dto.comment);
        actual.TheNewId.Should().Be(dto.TheNewId);
        actual.ReplyId.Should().Be(dto.ReplyId);
    }

    [Fact]
    public async Task add_adds_a_comment_to_another_comment_properly()
    {
        var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
        DbContext.Save(category);
        var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
        DbContext.Save(tag1);
        var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
        DbContext.Save(author);
        var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
        DbContext.Save(newspaper);
        var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
        DbContext.Save(newspaperCategory);
        var theNew = new TheNewBuilder(author.Id, newspaperCategory.Id)
            .WithTitle("پرسپولیس در لیگ")
            .WithDesciption("پرسپولیس قهرمان لیگ شد")
            .WithRate(10)
            .WithTheNewTags(tag1.Id)
            .Build();
        DbContext.Save(theNew);
        var comment = new Comment
        {
            TheNewId = theNew.Id,
            comment = "dummy-description",
        };
        DbContext.Save(comment);
        var dto = new AddCommentDto
        {
            TheNewId = theNew.Id,
            comment = "dummy-description2",
            ReplyId = comment.Id
        };

        await _sut.Add(dto);

        var actual = await ReadContext.Set<Comment>().Where(_ => _.ReplyId != null).FirstOrDefaultAsync();
        actual.comment.Should().Be(dto.comment);
        actual.TheNewId.Should().Be(dto.TheNewId);
        actual.ReplyId.Should().Be(dto.ReplyId);
    }

    [Fact]
    public async Task Get_gets_all_main_news()
    {
        var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
        DbContext.Save(category);
        var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
        DbContext.Save(tag1);
        var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
        DbContext.Save(author);
        var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
        DbContext.Save(newspaper);
        var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
        DbContext.Save(newspaperCategory);
        var theNew = new TheNewBuilder(author.Id, newspaperCategory.Id)
            .WithTitle("پرسپولیس در لیگ")
            .WithDesciption("پرسپولیس قهرمان لیگ شد")
            .WithRate(10)
            .WithTheNewTags(tag1.Id)
            .Build();
        DbContext.Save(theNew);
        var comment = new Comment
        {
            TheNewId = theNew.Id,
            comment = "dummy-description",
        };
        DbContext.Save(comment);
        var comment2 = new Comment
        {
            TheNewId = theNew.Id,
            comment = "dummy-description2",
            ReplyId = comment.Id
        };
        DbContext.Save(comment2);
        var comment3 = new Comment
        {
            TheNewId = theNew.Id,
            comment = "dummy-description3",
            ReplyId = comment2.Id
        };
        DbContext.Save(comment3);

        var actual = await _sut.GetAll();

        actual.Should().ContainSingle(_ => _.Id == comment.Id
                                           && _.Comment == comment.comment
        );

    }

    [Fact]
    public async Task Get_gets_all_relies_news()
    {
        var category = new CategoryBuilder().WithRate(20).WithTitle("ورزشی").Build();
        DbContext.Save(category);
        var tag1 = new TagBuilder(category.Id).WithTitle("فوتبال").Build();
        DbContext.Save(tag1);
        var author = new AuthorBuilder().WithFullName("زهرااحمدی").Build();
        DbContext.Save(author);
        var newspaper = new NewspaperBuilder().WithTitle("طلوع").Build();
        DbContext.Save(newspaper);
        var newspaperCategory = new NewspaperCategoryBuilder(category.Id, newspaper.Id).Build();
        DbContext.Save(newspaperCategory);
        var theNew = new TheNewBuilder(author.Id, newspaperCategory.Id)
            .WithTitle("پرسپولیس در لیگ")
            .WithDesciption("پرسپولیس قهرمان لیگ شد")
            .WithRate(10)
            .WithTheNewTags(tag1.Id)
            .Build();
        DbContext.Save(theNew);
        var comment = new Comment
        {
            TheNewId = theNew.Id,
            comment = "dummy-description",
        };
        DbContext.Save(comment);
        var comment2 = new Comment
        {
            TheNewId = theNew.Id,
            comment = "dummy-description2",
            ReplyId = comment.Id
        };
        DbContext.Save(comment2);
        var comment3 = new Comment
        {
            TheNewId = theNew.Id,
            comment = "dummy-description3",
            ReplyId = comment.Id
        };
        DbContext.Save(comment3);

        var actual = await _sut.GetAllReplies(comment.Id);

        actual.Should().ContainSingle(_ => _.Id == comment2.Id
                                           && _.Descripton == comment2.comment
        ).And.ContainSingle(_ => _.Id == comment3.Id
                                 && _.Descripton == comment3.comment);
    }
}