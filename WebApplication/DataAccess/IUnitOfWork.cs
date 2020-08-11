using DataAccess.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : AbstractEntity;
    }
}