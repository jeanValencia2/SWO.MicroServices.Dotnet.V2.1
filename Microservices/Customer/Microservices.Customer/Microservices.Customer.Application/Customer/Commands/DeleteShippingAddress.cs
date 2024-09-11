using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Customer.Application.Customer.Dtos;
using Microservices.Customer.Domain.Entities;
using Microservices.Security.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Customer.Application.Customer.Commands;

public record DeleteShippingAddress : IRequest<Result<CustomerDto>>
{
    public Guid CustomerId { get; set; }    
    public Guid ShippingAddresId { get; set; }
}

public class DeleteShippingAddressHandler : IRequestHandler<DeleteShippingAddress, Result<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public DeleteShippingAddressHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<CustomerDto>> Handle(DeleteShippingAddress request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
           .Include(c => c.ShippingAddresses)
           .FirstOrDefaultAsync(c => c.Id == request.CustomerId);

        Guard.Against.NotFound(request.CustomerId, entity);

        var shippingAddresses = entity.ShippingAddresses?.Where(x => x.Id == request.ShippingAddresId).FirstOrDefault();
        Guard.Against.Null<ShippingAddress>(shippingAddresses);

        entity.RemoveShippingAddress(shippingAddresses);
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = _currentUser.UserName;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CustomerDto>(entity);
    }
}
