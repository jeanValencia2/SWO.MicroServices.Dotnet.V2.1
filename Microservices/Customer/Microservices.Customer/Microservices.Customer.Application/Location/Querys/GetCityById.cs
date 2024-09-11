using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Security.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Location.Querys;

public record GetCityById : IRequest<Result<CityDto>>
{
    public Guid Id { get; set; }
}

public class GetCityByIdHandler : IRequestHandler<GetCityById, Result<CityDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCityByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CityDto>> Handle(GetCityById request, CancellationToken cancellationToken)
    {
        var product = await _context.Cities
            .Where(x => x.Id == request.Id)
            .Include(c => c.State)
            .ProjectTo<CityDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return product ?? new CityDto { Id = Guid.Empty, State = new StateDto { Id = Guid.Empty } };
    }
}
