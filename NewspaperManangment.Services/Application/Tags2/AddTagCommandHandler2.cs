using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Application.Tags;
using NewspaperManangment.Services.Tags.Contracts;

namespace NewspaperManangment.Services.Application.Tags2;

public class AddTagCommandHandler2
    (TagRepository tagRepository,
        UnitOfWork unitOfWork)
    : ICommandHandler<AddTagCommand2,AddTagCommandHandler2.MyClass>
{
   
    public class MyClass
    {
        public string TenantId {
            get;
            set;
        }
        public string UserId {
            get;
            set;
        }
    }
    

    public async Task<MyClass> Handle(AddTagCommand2 command, BasicRequest? basicCommand = null)
    {
        var tag = new Tag
        {
            Title = command.Title,
            CategoryId = command.CategoryId,
        };
        
        tagRepository.Add(tag);
        await unitOfWork.Complete();
        return new MyClass
        {
            TenantId = basicCommand.TenantId!,
            UserId = basicCommand.UserId!
        };
    }
}