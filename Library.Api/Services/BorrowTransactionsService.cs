using Library.Api.Infrastructure.Interfaces;
using Library.Api.Infrastructure.Models;
using Library.Api.Services.Interfaces;

namespace Library.Api.Services
{
    public class BorrowTransactionsService : IBorrowTransactionsService
    {
        private readonly IBorrowTransactionsRepository _borrowTransactionsRepository;
        private readonly IBooksService _booksService;
        private readonly IMembersService _membersService;

        public BorrowTransactionsService(IBorrowTransactionsRepository borrowTransactionsRepository, IBooksService booksService, IMembersService membersService)
        {
            _borrowTransactionsRepository = borrowTransactionsRepository ?? throw new ArgumentNullException(nameof(borrowTransactionsRepository));
            _booksService = booksService ?? throw new ArgumentNullException(nameof(booksService));
            _membersService = membersService ?? throw new ArgumentNullException(nameof(membersService));
        }

        public async Task<bool> Add(BorrowTransaction borrowTransaction)
        {
            var book = await _booksService.Get(borrowTransaction.BookId);

            if (book == null || !book.IsAvailable)
            {
                return false;
            }

            var member = _membersService.Get(borrowTransaction.MemberId);

            if (member == null)
            {
                return false;
            }

            return await _borrowTransactionsRepository.Add(borrowTransaction);
        }

        public async Task<IEnumerable<BorrowTransaction>> GetByMemberId(int memberId)
        {
            return await _borrowTransactionsRepository.GetByMemberId(memberId);
        }

        public async Task<bool> Update(int id, DateTime returnDate)
        {
            var transaction = await _borrowTransactionsRepository.GetById(id);

            if (transaction == null)
            {
                return false;
            }


            return await _borrowTransactionsRepository.Update(id, returnDate);
        }
    }
}
