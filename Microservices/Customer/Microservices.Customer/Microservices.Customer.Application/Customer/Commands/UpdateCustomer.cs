using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Customer.Application.Customer.Dtos;
using Microservices.Customer.Domain.Entities;
using Microservices.Security.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.EventSourcing;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Customer.Application.Customer.Commands;

public  record UpdateCustomer : IRequest<Result<CustomerDto>>
{
    public Guid Id  { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool IsVip { get; set; }
}

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomer, Result<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public UpdateCustomerHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<CustomerDto>> Handle(UpdateCustomer request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Email = request.Email;
        entity.IsVip = request.IsVip;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = _currentUser.UserName;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CustomerDto>(entity);
    }
}

