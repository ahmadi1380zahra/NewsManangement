public interface IQuery
{
}

public interface IQueryHandler<in TQuery,TResult> where TQuery : IQuery
{
    Task<TResult> Handle(TQuery query, BasicCommand? basicCommand = null);
}

public interface IQueryHandler<TResult>   : IQuery
{
    Task<TResult> Handle( BasicCommand? basicCommand = null);
}
