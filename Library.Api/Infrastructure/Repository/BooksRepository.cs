using Library.Api.Infrastructure.Interfaces;
using Library.Api.Infrastructure.Models;

namespace Library.Api.Infrastructure.Repository
{
    public class BooksRepository : GenericRepository<Book>
    {
        public BooksRepository(IConfiguration configuration, IDbHelperService dbHelperService) : base(configuration, dbHelperService)
        {
        }
    }
}
