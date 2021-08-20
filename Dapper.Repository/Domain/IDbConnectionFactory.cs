using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Dapper.Repository.Domain
{
    public interface IDbConnectionFactory
    {
        [return: NotNull]
        IDbConnection CreateDbConnection();
        [return: NotNull]
        TransactionScope CreateTransactionScope();
    }
}