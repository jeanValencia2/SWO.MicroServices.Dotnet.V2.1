using AutoMapper;
using MediatR;
using Microservices.Carts.Application.Common.Interfaces;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Carts.Application.Products.Commands;

public class UpdateProducts : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class UpdateProductsHandler : IRequestHandler<UpdateProducts, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProductsHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(UpdateProducts request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
        };
        
        var response = await _context.ProductRepository.UpdateAsync(entity);
        return response != null;
    }
}
