using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Location.Commands;

public record DeleteState : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}

public class DeleteStateHandler : IRequestHandler<DeleteState, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteStateHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteState request, CancellationToken cancellationToken)
    {
        var entity = await _context.States
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.States.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
