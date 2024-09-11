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

public record UpdateShippingAddress : IRequest<Result<CustomerDto>>
{
    public Guid CustomerId { get; set; }
    public Guid ShippingAddresId { get; set; }
    public string? NameAddress { get; set; }
    public string? Address { get; set; }
    public Guid CityId { get; set; }
}

public class UpdateShippingAddressHandler : IRequestHandler<UpdateShippingAddress, Result<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public UpdateShippingAddressHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<CustomerDto>> Handle(UpdateShippingAddress request, CancellationToken cancellationToken)
    {
        var entity = await  _context.Customers
            .Include(c => c.ShippingAddresses)
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId);
        var city = await _context.Cities
            .FindAsync(new object[] { request.CityId }, cancellationToken);

        Guard.Against.NotFound(request.CustomerId, entity);
        Guard.Against.NotFound(request.CityId, city);
        Guard.Against.NullOrEmpty(request.NameAddress);
        Guard.Against.NullOrEmpty(request.Address);
        Guard.Against.NullOrEmpty(request.CityId);

        var shippingAddresses = entity.ShippingAddresses?.Where(x => x.Id == request.ShippingAddresId).FirstOrDefault();
        Guard.Against.Null<ShippingAddress>(shippingAddresses);

        shippingAddresses.Name = request.NameAddress;
        shippingAddresses.Address = request.Address;
        shippingAddresses.City = city;

        entity.UpdateShippingAddress(request.ShippingAddresId, shippingAddresses);
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = _currentUser.UserName;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CustomerDto>(entity);
    }
}