using NewspaperManangment.Services.Application.Tags2;

namespace NewspaperManangment.Services.Application.Tag3;

public class AddTagCommand3 : ICommand
{
    public string Title { get; set; }
    public int CategoryId { get; set; }
}