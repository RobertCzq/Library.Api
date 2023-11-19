namespace Library.Api.Infrastructure.Interfaces
{
    /// <summary>
    /// Using generic repository from 
    /// https://medium.com/@ZuraizAhmedShehzad/mastering-the-dapper-orm-with-the-generic-repository-pattern-8049eb9de43b
    /// as inspiration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}
