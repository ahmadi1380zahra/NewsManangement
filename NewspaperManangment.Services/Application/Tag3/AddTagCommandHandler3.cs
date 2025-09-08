using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Application.Tags;
using NewspaperManangment.Services.Tags.Contracts;

namespace NewspaperManangment.Services.Application.Tag3;

public class AddTagCommandHandler3
    (TagRepository tagRepository,
        UnitOfWork unitOfWork)
    : ICommandHandler<AddTagCommand3>
{
    public async Task Handle(AddTagCommand3 command, BasicRequest? basic = null)
    {
        var tag = new Tag
        {
            Title = command.Title,
            CategoryId = command.CategoryId,
        };
        
        tagRepository.Add(tag);
        await unitOfWork.Complete();
        
    }
}