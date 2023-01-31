﻿using System;
using System.Data;
using System.Linq;

using GizmoDALV2;

namespace Gizmo.DAL.Contexts
{
    public class MySqlDbContext : IGizmoDBContext
    {
        private readonly string _connectionString;

        public MySqlDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool IsEventsCached { get; set; }

        public IDatabaseTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public IDatabaseTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public bool CredentialsIsPasswordValid(string password, byte[] salt, byte[] pwdHash)
        {
            throw new NotImplementedException();
        }

        public TEntity DemandFind<TEntity>(int entityKey) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public TEntity DemandFind<TEntity>(object[] entityKeys) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> QueryableSet<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
