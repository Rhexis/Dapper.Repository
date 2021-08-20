using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Transactions;
using Dapper.Contrib.Extensions;
using Dapper.Repository.Domain;
using Dapper.Repository.Internal.Extensions;

namespace Dapper.Repository.Internal.Domain
{
    internal class DapperRepository : IRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDbConnection _db;

        public DapperRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _db = _dbConnectionFactory.CreateDbConnection();
        }
        
        public long Insert<T>(T t) where T : Entity
        {
            return _db.Insert(t);
        }

        public void Insert<T>(IEnumerable<T> t) where T : Entity
        {
            _db.Insert(t);
        }

        public void Update<T>(T t) where T : Entity
        {
            _db.Update(t);
        }

        public void Delete<T>(T t) where T : Entity
        {
            _db.Delete(t);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _db.GetAll<T>();
        }

        public IEnumerable<T> Query<T>(string sql, object? param = null)
        {
            return _db.Query<T>(sql, param);
        }

        public IEnumerable<T> Query<T>(Expression<Func<T, object>> prop, object val) where T : Entity
        {
            return _db.Query<T>
            (
                BuildSelect(prop),
                new {_prop = val}
            );
        }

        public T QuerySingle<T>(string sql, object? param = null)
        {
            return _db.QuerySingle<T>(sql, param);
        }

        public T QuerySingle<T>(Expression<Func<T, object>> prop, object val) where T : Entity
        {
            return _db.QuerySingle<T>
            (
                BuildSelect(prop),
                new {_prop = val}
            );
        }

        public T? QuerySingleOrDefault<T>(string sql, object? param = null)
        {
            return _db.QuerySingleOrDefault<T>(sql, param);
        }

        public T? QuerySingleOrDefault<T>(Expression<Func<T, object>> prop, object val) where T : Entity
        {
            return _db.QuerySingleOrDefault<T>
            (
                BuildSelect(prop),
                new {_prop = val}
            );
        }

        public TransactionScope CreateTransaction()
        {
            return _dbConnectionFactory.CreateTransactionScope();
        }
        
        private static string BuildSelect<T>(Expression<Func<T, object>> prop)
        {
            return $"SELECT * FROM {typeof(T).GetAttributeValue((TableAttribute ta) => ta.Name)} WHERE {GetMemberName(prop)} = @_prop";
        }

        private static string GetMemberName<T>(Expression<Func<T, object>> prop)
        {
            if (prop.Body is MemberExpression member) return member.Member.Name;
            UnaryExpression unary = (UnaryExpression)prop.Body;
            member = (unary.Operand as MemberExpression)!;
            return member.Member.Name;
        }
    }
}