using System.Collections.Concurrent;
using Atos.Crm.Abstractions.DataAccess;

namespace Atos.Crm.Infrastructure.DataAccess;

public class DbContext<TAggregateRoot> : IDbContext<TAggregateRoot>
{
    public IDictionary<int, TAggregateRoot> Db { get; set; } = new ConcurrentDictionary<int, TAggregateRoot>();
    public int MaxID { get; set; } = 0;
}