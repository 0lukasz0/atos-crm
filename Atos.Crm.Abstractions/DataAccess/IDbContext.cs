namespace Atos.Crm.Abstractions.DataAccess;

public interface IDbContext<TAggregateRoot>
{
    IDictionary<int, TAggregateRoot> Db { get; set; }
    int MaxID { get; set; }
}