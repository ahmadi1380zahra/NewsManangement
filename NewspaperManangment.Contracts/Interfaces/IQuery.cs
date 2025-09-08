public interface IQuery
{
}

public interface IQueryHandler<in TFilter,TResult> where TFilter : IQuery
{
    Task<TResult> Handle(TFilter query, BasicRequest? basicRequest = null);
}

public interface ISimpleQueryHandler<TResult>   : IQuery
{
    Task<TResult> Handle( BasicRequest? basicRequest = null);
}
