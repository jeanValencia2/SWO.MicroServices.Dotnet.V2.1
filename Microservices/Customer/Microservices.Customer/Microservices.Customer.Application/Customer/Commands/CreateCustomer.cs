using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Customer.Application.Customer.Dtos;
using Microservices.Customer.Domain.Entities;
using Microservices.Security.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Setup.Services;
using Aggregates = Microservices.Customer.Domain.Aggregates;

namespace Microservices.Customer.Application.Customer.Commands;

public record CreateCustomer : IRequest<Result<CustomerDto>>
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool IsVip { get; set; }

    public List<ShippingAddressAddDto>? ShippingAddresses { get; set; }
}

public class CreateCustomerHandler : IRequestHandler<CreateCustomer, Result<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly ICurrentUser _currentUser;

    public CreateCustomerHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<CustomerDto>> Handle(CreateCustomer request, CancellationToken cancellationToken)
    {
        Guard.Against.Null<List<ShippingAddressAddDto>>(request.ShippingAddresses);
        Guard.Against.NullOrEmpty(request.Name);
        Guard.Against.NullOrEmpty(request.Email);

        List<Guid> cityIds = request.ShippingAddresses.Select(s => s.CityId).ToList();
        var cities = _context.Cities
            .Where(c => cityIds.Contains(c.Id))
            .ToList();

        if (cityIds.Count != cities.Count)
            throw new ArgumentNullException("Invalid cities");

        var entity = new Aggregates.Customer
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            IsVip = request.IsVip,
            Created = DateTime.Now,
            CreatedBy = _currentUser.UserName
        };

        foreach (var shippingAddress in request.ShippingAddresses)
        {
            var city = cities.FirstOrDefault(x => x.Id == shippingAddress.CityId);

            entity.AddShippingAddress(new ShippingAddress
            {
                Id = Guid.NewGuid(),
                Name = shippingAddress.Name,
                Address = shippingAddress.Address,
                City = city!
            });
        }

        _context.Customers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);        

        return _mapper.Map<CustomerDto>(entity);
    }
}
