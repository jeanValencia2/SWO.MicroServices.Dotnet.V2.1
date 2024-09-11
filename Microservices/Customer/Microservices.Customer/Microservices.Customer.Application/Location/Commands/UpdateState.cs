
using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Customer.Application.Location.Commands;

public record UpdateState : IRequest<Result<StateDto>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class UpdateStateHandler : IRequestHandler<UpdateState, Result<StateDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public UpdateStateHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<StateDto>> Handle(UpdateState request, CancellationToken cancellationToken)
    {
        var entity = await _context.States
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = _currentUser.UserName;
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StateDto>(entity);
    }
}
