using System.Reflection;

namespace Library.Api.Infrastructure.Interfaces
{
    public interface IDbHelperService
    {
        string GetKeyPropertyName<T>();
        IEnumerable<PropertyInfo> GetProperties<T>(bool excludeKey = false);
        string GetPropertyNames<T>(bool excludeKey = false);
        string GetColumns<T>(bool excludeKey = false);
        string GetTableName<T>();
    }
}
