namespace Library.Api.Services.Interfaces
{
    public interface IGenericService<T>
    {
        Task<T> Get(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}
