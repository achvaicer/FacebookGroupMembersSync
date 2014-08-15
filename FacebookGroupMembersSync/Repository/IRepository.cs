using System;
namespace FacebookGroupMembersSync.Repository
{
    interface IRepository
    {
        System.Collections.Generic.IEnumerable<TEntity> All<TEntity>() where TEntity : class, new();
        void Delete<TEntity>(object key) where TEntity : class, new();
        bool Exists<TEntity>(object key) where TEntity : class, new();
        void Save<TEntity>(TEntity item) where TEntity : class, new();
        TEntity Single<TEntity>(object key) where TEntity : class, new();
    }
}
