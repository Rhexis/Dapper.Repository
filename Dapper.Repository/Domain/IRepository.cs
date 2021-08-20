using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;

namespace Dapper.Repository.Domain
{
    public interface IRepository
    {
        long Insert<T>(T t) where T : Entity;
        void Insert<T>(IEnumerable<T> t) where T : Entity;
        void Update<T>(T t) where T : Entity;
        void Delete<T>(T t) where T : Entity;
        IEnumerable<T> GetAll<T>() where T : Entity;
        IEnumerable<T> Query<T>(string sql, object? param = null);
        IEnumerable<T> Query<T>(Expression<Func<T, object>> prop, object val) where T : Entity;
        T QuerySingle<T>(string sql, object? param = null);
        T QuerySingle<T>(Expression<Func<T, object>> prop, object val) where T : Entity;
        T? QuerySingleOrDefault<T>(string sql, object? param = null);
        T? QuerySingleOrDefault<T>(Expression<Func<T, object>> prop, object val) where T : Entity;
        TransactionScope CreateTransaction();
    }
}