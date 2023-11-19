using Library.Api.Infrastructure.Models;

namespace Library.Api.Infrastructure.Interfaces
{
    public interface IBorrowTransactionsRepository
    {
        Task<IEnumerable<BorrowTransaction>> GetByMemberId(int memberId);
        Task<bool> Add(BorrowTransaction borrowTransaction);
        Task<bool> Update(int id, int bookId, DateTime returnDate);
        Task<BorrowTransaction?> GetById(int Id);
    }
}
