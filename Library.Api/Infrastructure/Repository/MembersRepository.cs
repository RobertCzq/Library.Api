using Library.Api.Infrastructure.Models;

namespace Library.Api.Infrastructure.Repository
{
    public class MembersRepository : GenericRepository<Member>
    {
        public MembersRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
