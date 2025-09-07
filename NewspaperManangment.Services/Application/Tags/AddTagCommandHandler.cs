using NewspaperManangment.Contracts.Interfaces;
using NewspaperManangment.Entities;
using NewspaperManangment.Services.Tags.Contracts;

namespace NewspaperManangment.Services.Application.Tags;

public class AddTagCommandHandler
    (TagRepository tagRepository,
        UnitOfWork unitOfWork)
    : ICommandHandler<AddTagCommand,int>
{
    public async Task<int> Handle(AddTagCommand command, BasicCommand? basicCommand)
    {
        var tag = new Tag
        {
            Title = command.Title,
            CategoryId = command.CategoryId,
        };
        
        tagRepository.Add(tag);
        await unitOfWork.Complete();
        return tag.Id;
    }
}