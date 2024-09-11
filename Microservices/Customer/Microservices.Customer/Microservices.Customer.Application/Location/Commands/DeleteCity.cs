using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Location.Commands;

public record DeleteCity : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}

public class DeleteCityHandler : IRequestHandler<DeleteCity, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteCityHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(DeleteCity request, CancellationToken cancellationToken)
    {
        var entity = await _context.Cities
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

         _context.Cities.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
