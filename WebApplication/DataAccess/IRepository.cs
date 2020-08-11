using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess
{
    public interface IRepository<T> where T : AbstractEntity
    {
        T Find(object id);
        IEnumerable<T> All { get; }
        T Insert(T entity, bool saveChanges = true);
    }
}