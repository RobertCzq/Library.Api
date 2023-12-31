﻿using Library.Api.Infrastructure.Models;

namespace Library.Api.Services.Interfaces
{
    public interface IBorrowTransactionsService
    {
        Task<IEnumerable<BorrowTransaction>> GetByMemberId(int memberId);
        Task<bool> Add(BorrowTransaction borrowTransaction);
        Task<bool> Update(int id, DateTime returnDate);
    }
}
