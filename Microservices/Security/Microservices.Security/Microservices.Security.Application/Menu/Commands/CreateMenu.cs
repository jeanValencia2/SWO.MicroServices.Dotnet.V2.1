using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Application.Identity.Dtos;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Menu.Commands;

public record CreateMenu : IRequest<Result<MenuDto>>
{
    public required string Path { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Class { get; set; }
    public string? Badge { get; set; }
    public string? BadgeClass { get; set; }
    public Boolean IsExternalLink { get; }
    public Boolean IsParent { get; }
    public Boolean IsLocked { get; set; }
    public Guid? MenuId { get; set; }
    public Guid ApplicationId { get; set; }
    public List<string>? Roles { get; set; }   
}

public class CreateMenuHandler : IRequestHandler<CreateMenu, Result<MenuDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateMenuHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<MenuDto>> Handle(CreateMenu request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.Menu>(request);

        Guard.Against.Null<Domain.Entities.Menu>(entity);
        entity.Id = Guid.NewGuid();

        _context.Menus.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<MenuDto>(entity);
    }
}
