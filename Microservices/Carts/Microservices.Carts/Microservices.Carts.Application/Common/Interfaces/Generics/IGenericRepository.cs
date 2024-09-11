namespace Microservices.Carts.Application.Common.Interfaces.Generics;

public interface IGenericRepository<T> : IGetRepository<T>, IGetAllRepository<T>, IAddRepository<T>, IDeleteRepository<T>, IUpdateRepository<T> where T : class
{
}