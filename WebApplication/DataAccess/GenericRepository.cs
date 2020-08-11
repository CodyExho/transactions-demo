using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess
{
    public class GenericRepository<T> : IRepository<T> where T : AbstractEntity
    {
        private UnitOfWork _unitOfWork;
        public GenericRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public T Find(object id)
        {
            var entities = _unitOfWork.MemoryCache.Get<Dictionary<string, T>>(typeof(T).ToString());
            if (entities == null)
            {
                return null;
            }

            return entities.ContainsKey(id.ToString()) ? entities[id.ToString()] : null;
        }

        public IEnumerable<T> All => _unitOfWork.MemoryCache.Get<Dictionary<string, T>>(typeof(T).ToString())?.Values;

        public T Insert(T entity, bool saveChanges = true)
        {
            var entities = _unitOfWork.MemoryCache.Get<Dictionary<string, T>>(typeof(T).ToString()) ?? new Dictionary<string, T>();
            entities[entity.Id] = entity;
            _unitOfWork.MemoryCache.Set(typeof(T).ToString(), entities);
            return entity;
        }
    }
}