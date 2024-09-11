namespace Microservices.Carts.Application.Common.Interfaces.Generics;

public interface IUpdateRepository<T> where T : class
{
    Task<T> UpdateAsync(T entity);
}
