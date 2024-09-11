namespace Microservices.Carts.Application.Common.Interfaces.Generics;

public interface IDeleteRepository<T> where T : class
{
    Task<bool> DeleteAsync(T entity);
}
