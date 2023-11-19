using Library.Api.Infrastructure.Interfaces;
using Library.Api.Infrastructure.Models;

namespace Library.Api.Infrastructure.Repository
{
    public class BorrowTransactionRepository : GenericRepository<Member>
    {
        public BorrowTransactionRepository(IConfiguration configuration, IDbHelperService dbHelperService) : base(configuration, dbHelperService)
        {
        }
    }
}
