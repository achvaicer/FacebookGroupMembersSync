using System;
using System.Collections.Generic;

namespace FacebookGroupMembersSync.Repository
{
    interface IRepository
    {
        IEnumerable<TEntity> All<TEntity>() where TEntity : class, new();
        void Delete<TEntity>(object key) where TEntity : class, new();
        bool Exists<TEntity>(object key) where TEntity : class, new();
        void Save<TEntity>(TEntity item) where TEntity : class, new();
        TEntity Single<TEntity>(object key) where TEntity : class, new();
		IEnumerable<TEntity> Where<TEntity>(IDictionary<string, object> query) where TEntity : class, new();
    }
}
