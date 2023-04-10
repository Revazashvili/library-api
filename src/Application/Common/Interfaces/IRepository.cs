namespace Application.Common.Interfaces;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(T entity,CancellationToken cancellationToken = default);
    void Update(T entity);
    Task DeleteAsync(int id,CancellationToken cancellationToken = default);
}