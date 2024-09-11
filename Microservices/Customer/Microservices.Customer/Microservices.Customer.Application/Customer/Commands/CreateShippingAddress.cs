using Ardalis.GuardClauses;
using AutoMapper;
using Azure.Core;
using k8s.KubeConfigModels;
using MediatR;
using Microservices.Customer.Application.Customer.Dtos;
using Microservices.Customer.Application.Location.Dtos;
using Microservices.Customer.Domain.Aggregates;
using Microservices.Customer.Domain.Entities;
using Microservices.Security.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Customer.Application.Customer.Commands;

public record CreateShippingAddress : IRequest<Result<CustomerDto>>
{
    public Guid CustomerId { get; set; }
    public string? NameAddress { get; set; }
    public string? Address { get; set; }
    public Guid CityId { get; set; }
}

public class CreateShippingAddressHandler : IRequestHandler<CreateShippingAddress, Result<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public CreateShippingAddressHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<CustomerDto>> Handle(CreateShippingAddress request, CancellationToken cancellationToken)
    {        
        var entity = await _context.Customers            
            .Include(c => c.ShippingAddresses)
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId);
        var city = await _context.Cities
            .FindAsync(new object[] { request.CityId }, cancellationToken);
                
        Guard.Against.NotFound(request.CustomerId, entity);
        Guard.Against.NotFound(request.CityId, city);
        Guard.Against.NullOrEmpty(request.NameAddress);
        Guard.Against.NullOrEmpty(request.Address);
        Guard.Against.NullOrEmpty(request.CityId);

        var newShippingAddress = new ShippingAddress()
        {
            Id = Guid.NewGuid(),
            Name = request.NameAddress,
            Address = request.Address,
            City = city,
            CustomerId = request.CustomerId,
        };

        // #BUG1: Revisar porque no se pueden insertar direcciones a un customer existente a través de su agregado
        // Error que presenta: The database operation was expected to affect 1 row(s), but actually affected 0 row(s); data may have been modified or deleted since entities were loaded. See https://go.microsoft.com/fwlink/?LinkId=527962 for information on understanding and handling optimistic concurrency exceptions.

        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = _currentUser.UserName;
        _context.ShippingAddresses.Add(newShippingAddress);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CustomerDto>(entity);
    }
}
