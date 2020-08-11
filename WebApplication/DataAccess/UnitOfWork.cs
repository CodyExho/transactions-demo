using System;
using System.Collections.Generic;
using DataAccess.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<string, object> _cachedRepos;
        
        public UnitOfWork(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
            _cachedRepos = new Dictionary<string, object>();
        }
        
        internal IMemoryCache MemoryCache { get; }

        public IRepository<T> GetRepository<T>() where T : AbstractEntity
        {
            var type = typeof(T).ToString();

            if (_cachedRepos.ContainsKey(type))
            {
                return _cachedRepos[type] as GenericRepository<T>;
            }

            _cachedRepos[type] = new GenericRepository<T>(this);
            return (GenericRepository<T>)_cachedRepos[type];
        }
    }
}