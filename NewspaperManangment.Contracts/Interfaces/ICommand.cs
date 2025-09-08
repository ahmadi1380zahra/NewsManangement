public interface ICommand
{
}
public interface ICommandHandler<in TCommand,TResult> where TCommand : ICommand
{
    Task<TResult> Handle(TCommand command, BasicRequest? basicCommand = null);
}
public interface ICommandHandler<in TCommand>
    where TCommand : ICommand
{
    Task Handle(TCommand command, BasicRequest? basic = null);
}
public readonly record struct Unit
{
    public static readonly Unit Value = new();
}
public class BasicRequest
{
    public string? UserId { get; set; }
    public string? TenantId { get; set; }
}