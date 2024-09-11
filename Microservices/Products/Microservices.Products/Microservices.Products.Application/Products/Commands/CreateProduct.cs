using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Products.Application.Common.Interfaces;
using Microservices.Products.Application.Products.Dtos;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Products.Application.Products.Commands;

public record CreateProduct : IRequest<Result<ProductDto>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }
}

public class CreateProductHandler : IRequestHandler<CreateProduct, Result<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public CreateProductHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<Result<ProductDto>> Handle(CreateProduct request, CancellationToken cancellationToken)
    {        
        Guard.Against.NullOrEmpty(request.Name);
        Guard.Against.NullOrEmpty(request.Description);
        Guard.Against.Null<int>(request.Stock);
        Guard.Against.Null<int>(request.Price);

        var entity = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Stock = request.Stock,
            Price = request.Price,
            Created = DateTime.Now,
            CreatedBy = "user2@correo.com" //_currentUser.UserName
        };        

        _context.Products.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(entity);
    }
}
