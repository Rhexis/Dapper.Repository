using Dapper.Contrib.Extensions;

namespace Dapper.Repository.Domain
{
    /// <summary>
    /// See : https://github.com/StackExchange/Dapper/tree/master/Dapper.Contrib
    /// </summary>
    public abstract class Entity
    {
        // Auto Generated Identity
        [Key] public long Id { get; set; }
    }
}