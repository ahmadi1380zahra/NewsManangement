public interface ICommand { }

public interface ICommandHandler<TCommand,TResult> where TCommand : ICommand
{
    Task<TResult> Handle(TCommand command, BasicCommand? basicCommand);
}

public class BasicCommand
{
    public string? UserId { get; set; }
    public string? TenantId { get; set; }
}