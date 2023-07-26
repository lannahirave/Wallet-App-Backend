namespace WAB.DAL.Repositories.Abstract;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();

    Task<T?> Get(int id);
    // Task<T> Create(T item);
    // Task<T> Update(T item);
    // Task Delete(int id);
}