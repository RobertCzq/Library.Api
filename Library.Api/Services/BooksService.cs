using Library.Api.Infrastructure.Interfaces;
using Library.Api.Infrastructure.Models;
using Library.Api.Services.Interfaces;

namespace Library.Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly IGenericRepository<Book> _booksRepository;

        public BooksService(IGenericRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository)); ;
        }

        public async Task<bool> Add(Book book)
        {
            return await _booksRepository.Add(book);
        }

        public async Task<bool> Delete(int id)
        {
            return await _booksRepository.Delete(id);
        }

        public async Task<Book> Get(int id)
        {
            return await _booksRepository.GetById(id);
        }

        public async Task<bool> Update(int id, Book book)
        {
            return await _booksRepository.Update(id, book);
        }
    }
}
