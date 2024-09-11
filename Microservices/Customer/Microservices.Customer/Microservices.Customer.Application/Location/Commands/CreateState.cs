using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Customer.Domain.Entities;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Customer.Application.Location.Commands;

public record CreateState : IRequest<Result<StateDto>>
{
    public string? Name { get; set; }
}

public class CreateStateHandler : IRequestHandler<CreateState, Result<StateDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public CreateStateHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<StateDto>> Handle(CreateState request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<State>(request);

        Guard.Against.Null<State>(entity);
        entity.Id = Guid.NewGuid();
        entity.Created = DateTime.Now;
        entity.CreatedBy = _currentUser.UserName;

        _context.States.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StateDto>(entity);
    }
}
