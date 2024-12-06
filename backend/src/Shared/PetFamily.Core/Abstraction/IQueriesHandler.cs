namespace PetFamily.Core.Abstraction;

public interface IQueriesHandler<TResponce, TQueries> where TQueries : IQueries
{
    Task<TResponce> Handle(TQueries query, CancellationToken token = default);
}

public interface IQueriesHandler<TResponce>
{
    Task<TResponce> Handle(CancellationToken token = default);
}