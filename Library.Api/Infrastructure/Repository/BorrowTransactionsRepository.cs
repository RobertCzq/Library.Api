using Dapper;
using Library.Api.Infrastructure.Interfaces;
using Library.Api.Infrastructure.Models;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Library.Api.Infrastructure.Repository
{
    public class BorrowTransactionsRepository : IBorrowTransactionsRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbHelperService _dbHelperService;

        public BorrowTransactionsRepository(IConfiguration configuration, IDbHelperService dbHelperService)
        {
            var connectionString = configuration.GetConnectionString("localDb");
            _connection = new SqliteConnection(connectionString);
            _dbHelperService = dbHelperService;
        }

        public async Task<bool> Add(BorrowTransaction borrowTransaction)
        {
            int rowsEffected = 0;
            _connection.Open();
            using (var tran = _connection.BeginTransaction())
            {
                try
                {
                    string borrowTransactionTable = _dbHelperService.GetTableName<BorrowTransaction>();
                    string booksTable = _dbHelperService.GetTableName<Book>();
                    string columns = _dbHelperService.GetColumns<BorrowTransaction>(excludeKey: true);
                    string properties = _dbHelperService.GetPropertyNames<BorrowTransaction>(excludeKey: true);
                    string query = $"INSERT INTO {borrowTransactionTable} ({columns}) VALUES ({properties})";
                    string updateQuery = $"UPDATE {booksTable} SET IsAvailable = 0 WHERE ID = {borrowTransaction.BookId}";

                    rowsEffected = await _connection.ExecuteAsync(query, borrowTransaction);
                    await _connection.ExecuteAsync(updateQuery, borrowTransaction);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }

            }

            return rowsEffected > 0 ? true : false;
        }

        public async Task<IEnumerable<BorrowTransaction>> GetByMemberId(int memberId)
        {
            try
            {
                string tableName = _dbHelperService.GetTableName<BorrowTransaction>();
                string query = $"SELECT * FROM {tableName} WHERE MemberID = {memberId}";

                return await _connection.QueryAsync<BorrowTransaction>(query);
            }
            catch (Exception ex) { }

            return new List<BorrowTransaction>();
        }

        public async Task<bool> Update(int id, DateTime returnDate)
        {
            int rowsEffected = 0;

            try
            {
                string borrowTransactionTable = _dbHelperService.GetTableName<BorrowTransaction>();

                string updateQuery = $"UPDATE {borrowTransactionTable} SET ReturnDate = @ReturnDate WHERE ID = {id}";

                rowsEffected = await _connection.ExecuteAsync(updateQuery, new { ReturnDate = returnDate });
            }
            catch (Exception ex)
            {
            }

            return rowsEffected > 0 ? true : false;
        }

        public async Task<BorrowTransaction?> GetById(int id)
        {
            try
            {
                string tableName = _dbHelperService.GetTableName<BorrowTransaction>();
                string query = $"SELECT * FROM {tableName} WHERE ID = {id}";

                return await _connection.QueryFirstOrDefaultAsync<BorrowTransaction>(query);
            }
            catch (Exception ex) { }

            return null;
        }
    }
}
