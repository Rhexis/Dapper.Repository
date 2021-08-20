using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Dapper.Repository.Domain;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace Dapper.Repository.Internal.Domain
{
    internal class DefaultDbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        
        public DefaultDbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IDbConnection CreateDbConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public TransactionScope CreateTransactionScope()
        {
            return new TransactionScope
            (
                TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.MaximumTimeout
                }
            );
        }
    }
}