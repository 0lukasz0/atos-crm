using Atos.Crm.Core;

namespace Atos.Crm.Abstractions.DataAccess;

public interface IRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
{
    Task<int> CreateAsync(TAggregateRoot model, CancellationToken cancellationToken = default);

    Task<TAggregateRoot?> GetAsync(int id, CancellationToken cancellationToken = default);

    IQueryable<TAggregateRoot> Query();

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}