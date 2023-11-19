using Library.Api.Infrastructure.Models;
using Library.Api.Infrastructure.Repository.Interfaces;
using Library.Api.Services.Interfaces;

namespace Library.Api.Services
{
    public class MembersService : IMembersService
    {
        private readonly IGenericRepository<Member> _membersRepository;

        public MembersService(IGenericRepository<Member> membersRepository)
        {
            _membersRepository = membersRepository ?? throw new ArgumentNullException(nameof(membersRepository)); ;
        }

        public async Task<bool> Add(Member member)
        {
            return await _membersRepository.Add(member);
        }

        public async Task<bool> Delete(int id)
        {
            return await _membersRepository.Delete(id);
        }

        public async Task<Member> Get(int id)
        {
            return await _membersRepository.GetById(id);
        }

        public async Task<bool> Update(int id, Member member)
        {
            return await _membersRepository.Update(id, member);
        }
    }
}
