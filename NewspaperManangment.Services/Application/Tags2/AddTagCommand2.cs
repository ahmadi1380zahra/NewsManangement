namespace NewspaperManangment.Services.Application.Tags2;

public class AddTagCommand2 : ICommand
{
    public string Title { get; set; }
    public int CategoryId { get; set; }
}