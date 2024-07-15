using NewspaperManangment.Persistance.EF;
using NewspaperManangment.Persistance.EF.Comments;
using NewspaperManangment.Services.Comments;
using NewspaperManangment.Services.Comments.Contracts;

namespace NewspaperManangement.Test.Tools.Comments;

public static class CommentServiceFactory
{
    public static CommentService Create(EFDataContext context)
    {
        var unitOfWork = new EFUnitOfWork(context);
        var repository = new EFCommentRepository(context);
        return new CommentAppService(unitOfWork, repository);
    }
}