﻿using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using MongoDB.Driver.Builders;

namespace FacebookGroupMembersSync.Repository
{
    public class MongoRepository : IRepository
    {
        private readonly RepositoryKeys _keys;
        private readonly string _connectionString;
        private readonly string _databaseName;

        private readonly MongoServer _server;
        private readonly MongoClient _client;
        private readonly MongoDatabase _db;

        public MongoRepository(RepositoryKeys keys, string connectionString, string databaseName)
        {
            _keys = keys;
            _connectionString = connectionString;
            _databaseName = databaseName;

            _client = new MongoClient(connectionString);
            _server = _client.GetServer();
            _db = _server.GetDatabase(databaseName);
        }

        public TEntity Single<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = new QueryDocument("_id", BsonValue.Create(key));
            return collection.FindOneAs<TEntity>(query);
        }

        public IEnumerable<TEntity> All<TEntity>() where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var entity = collection.FindAllAs<TEntity>();
            return entity;
        }

		public IEnumerable<TEntity> Where<TEntity>(IDictionary<string, object> query) where TEntity : class, new()
		{
			var collection = GetCollection<TEntity>();
			var entity = collection.FindAs<TEntity> (new QueryDocument (query));
			return entity;
		}

        public bool Exists<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = new QueryDocument("_id", BsonValue.Create(key));
            var entity = collection.FindOneAs<TEntity>(query);
            return (entity != null);
        }

        public void Save<TEntity>(TEntity item) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            collection.Save(item);
        }

        public void Delete<TEntity>(object key) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var query = new QueryDocument("_id", BsonValue.Create(key));
            collection.Remove(query);
        }

        private MongoCollection GetCollection<TEntity>()
        {
            return _db.GetCollection(typeof(TEntity).Name);
        }
    }
}