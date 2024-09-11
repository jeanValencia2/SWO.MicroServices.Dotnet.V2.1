using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Customer.Domain.Entities;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Customer.Application.Location.Commands;

public record CreateCity : IRequest<Result<CityDto>>
{
    public string? Name { get; set; }
    public Guid StateId { get; set; }
}

public class CreateCityHandler : IRequestHandler<CreateCity, Result<CityDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public CreateCityHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<CityDto>> Handle(CreateCity request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<City>(request);

        Guard.Against.Null<City>(entity);
        entity.Id = Guid.NewGuid();
        entity.Created = DateTime.Now;
        entity.CreatedBy = _currentUser.UserName;

        _context.Cities.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CityDto>(entity);
    }
}
