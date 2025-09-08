using System.Security.Cryptography;

namespace NewspaperManangment.Services.Application.Tags;

public class AddTagCommand : ICommand//<int>
{
    public string Title { get; set; }
    public int CategoryId { get; set; }
}