using Dapper;
using Library.Api.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;

namespace Library.Api.Infrastructure.Repository
{
    /// <inheritdoc />
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _connection;
        private readonly IDbHelperService _dbHelperService;

        public GenericRepository(IConfiguration configuration, IDbHelperService dbHelperService)
        {
            var connectionString = configuration.GetConnectionString("localDb");
            _connection = new SqliteConnection(connectionString);
            _dbHelperService = dbHelperService;
        }

        public async Task<bool> Add(T entity)
        {
            string tableName = _dbHelperService.GetTableName<T>();
            string columns = _dbHelperService.GetColumns<T>(excludeKey: true);
            string properties = _dbHelperService.GetPropertyNames<T>(excludeKey: true);
            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

            return await _connection.ExecuteAsync(query, entity) > 0;
        }

        public async Task<bool> Delete(int id)
        {
            string tableName = _dbHelperService.GetTableName<T>();
            string query = $"DELETE FROM {tableName} WHERE ID = {id}";

            return await _connection.ExecuteAsync(query) > 0;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            string tableName = _dbHelperService.GetTableName<T>();
            string query = $"SELECT * FROM {tableName}";

            return await _connection.QueryAsync<T>(query);
        }

        public async Task<T> GetById(int id)
        {
            string tableName = _dbHelperService.GetTableName<T>();
            string query = $"SELECT * FROM {tableName} WHERE ID = {id}";

            return await _connection.QueryFirstOrDefaultAsync<T>(query);
        }

        public async Task<bool> Update(int id, T entity)
        {
            string tableName = _dbHelperService.GetTableName<T>();

            StringBuilder query = new();
            query.Append($"UPDATE {tableName} SET ");

            foreach (var property in _dbHelperService.GetProperties<T>(true))
            {
                var columnAttr = property.GetCustomAttribute<ColumnAttribute>();
                string propertyName = property.Name;
                string columnName = columnAttr.Name;

                query.Append($"{columnName} = @{propertyName},");
            }

            query.Remove(query.Length - 1, 1);
            query.Append($" WHERE ID = {id}");

            return await _connection.ExecuteAsync(query.ToString(), entity) > 0;
        }
    }
}
