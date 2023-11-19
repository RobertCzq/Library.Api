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
            int rowsEffected = 0;
            try
            {
                string tableName = _dbHelperService.GetTableName<T>();
                string columns = _dbHelperService.GetColumns<T>(excludeKey: true);
                string properties = _dbHelperService.GetPropertyNames<T>(excludeKey: true);
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

                rowsEffected = await _connection.ExecuteAsync(query, entity);
            }
            catch (Exception ex) { }

            return rowsEffected > 0 ? true : false;
        }

        public async Task<bool> Delete(int id)
        {
            int rowsEffected = 0;
            try
            {
                string tableName = _dbHelperService.GetTableName<T>();

                string query = $"DELETE FROM {tableName} WHERE ID = {id}";

                rowsEffected = await _connection.ExecuteAsync(query);
            }
            catch (Exception ex) { }

            return rowsEffected > 0 ? true : false;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IEnumerable<T> result = null;
            try
            {
                string tableName = _dbHelperService.GetTableName<T>();
                string query = $"SELECT * FROM {tableName}";

                result = await _connection.QueryAsync<T>(query);
            }
            catch (Exception ex) { }

            return result;
        }

        public async Task<T> GetById(int id)
        {
            IEnumerable<T> result = null;
            try
            {
                string tableName = _dbHelperService.GetTableName<T>();
                string query = $"SELECT * FROM {tableName} WHERE ID = {id}";

                result = await _connection.QueryAsync<T>(query);
            }
            catch (Exception ex) { }

            return result.FirstOrDefault();
        }

        public async Task<bool> Update(int id, T entity)
        {
            int rowsEffected = 0;
            try
            {
                string tableName = _dbHelperService.GetTableName<T>();

                StringBuilder query = new StringBuilder();
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

                rowsEffected = await _connection.ExecuteAsync(query.ToString(), entity);
            }
            catch (Exception ex) { }

            return rowsEffected > 0 ? true : false;
        }


    }
}
