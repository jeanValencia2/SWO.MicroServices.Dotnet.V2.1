using Ardalis.GuardClauses;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Customer.Commands;

public record DeleteCustomer : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomer, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteCustomer request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Customers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
