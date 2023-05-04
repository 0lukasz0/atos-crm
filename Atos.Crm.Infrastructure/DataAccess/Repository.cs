using Atos.Crm.Abstractions.DataAccess;
using Atos.Crm.Core;

namespace Atos.Crm.Infrastructure.DataAccess;

// note: mock implementation
public class Repository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
    private readonly IDbContext<TAggregateRoot> _dbContext;

    public Repository(IDbContext<TAggregateRoot> dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateAsync(TAggregateRoot model, CancellationToken cancellationToken = default)
    {
        if (model.Id != 0)
            throw new InvalidOperationException("Cannot add existing object to database.");

        var id = ++_dbContext.MaxID;
        model.Id = id;

        _dbContext.Db.Add(id, model);

        return id;
    }

    public async Task<TAggregateRoot?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        if (_dbContext.Db.ContainsKey(id))
            return _dbContext.Db[id];
        
        return null;
    }

    public IQueryable<TAggregateRoot> Query()
    {
        return _dbContext.Db.Select(k => k.Value).AsQueryable();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Db.Remove(id);
    }
}