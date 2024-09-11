using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Products.Application.Common.Interfaces;
using Microservices.Products.Application.Products.Dtos;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;
using SWO.Microservices.Dotnet.Shared.Comunication.Publisher.Domain;
using SWO.Microservices.Dotnet.Shared.Comunication.Publisher.Integration;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Products.Application.Products.Commands;

public record UpdateProduct : IRequest<Result<ProductDto>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }
}

public class UpdateProductHandler : IRequestHandler<UpdateProduct, Result<ProductDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    private readonly IDomainMessagePublisher _domainMessagePublisher;
    private readonly IIntegrationMessagePublisher _integrationMessagePublisher;

    public UpdateProductHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser, IDomainMessagePublisher domainMessagePublisher, IIntegrationMessagePublisher integrationMessagePublisher)
    {
        _context = context;
        _mapper = mapper;
        _currentUser = currentUser;
        _domainMessagePublisher = domainMessagePublisher;
        _integrationMessagePublisher = integrationMessagePublisher;
    }

    public async Task<Result<ProductDto>> Handle(UpdateProduct request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        Guard.Against.NullOrEmpty(request.Name);
        Guard.Against.NullOrEmpty(request.Description);
        Guard.Against.Null<int>(request.Stock);
        Guard.Against.Null<int>(request.Price);

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.Stock = request.Stock;
        entity.Price = request.Price;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "user3@correo.com"; //_currentUser.UserName_currentUser.UserName;

        await _context.SaveChangesAsync(cancellationToken);

        _ = _domainMessagePublisher.Publish(entity, routingKey: "external");
        _ = _integrationMessagePublisher.Publish(entity, routingKey: "external", cancellationToken: cancellationToken);

        return _mapper.Map<ProductDto>(entity);
    }
}