using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microservices.Customer.Application.Customer.Dtos;
using Microservices.Security.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Application.Customer.Querys;

public class GetCustomerById : IRequest<Result<CustomerDto>>
{
    public Guid Id { get; set; }
}

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, Result<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<CustomerDto>> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .Where(x => x.Id == request.Id)
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return customer!;
    }
}
