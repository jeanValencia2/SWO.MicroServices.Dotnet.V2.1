namespace Microservices.Carts.Application.Common.Interfaces.Generics;

public interface IGetAllRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
}
