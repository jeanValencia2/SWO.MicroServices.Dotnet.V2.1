using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Security.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Location.Querys;

public record GetStateById : IRequest<Result<StateDto>>
{
    public Guid Id { get; set; }
}

public class GetStateByIdHandler : IRequestHandler<GetStateById, Result<StateDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetStateByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<StateDto>> Handle(GetStateById request, CancellationToken cancellationToken)
    {
        var product = await _context.States
            .Where(x => x.Id == request.Id)
            .ProjectTo<StateDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return product ?? new StateDto { Id = Guid.Empty };
    }
}
