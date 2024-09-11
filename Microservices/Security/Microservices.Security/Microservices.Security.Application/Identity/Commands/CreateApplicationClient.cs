using AutoMapper;
using MediatR;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Application.Identity.Dtos;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Security.Application.Identity.Commands;

public  record CreateApplicationClient : IRequest<Result<ApplicationClientDto>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ReturnUrl { get; set; }
}

public class CreateApplicationClientHandler : IRequestHandler<CreateApplicationClient, Result<ApplicationClientDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateApplicationClientHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ApplicationClientDto>> Handle(CreateApplicationClient request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ApplicationClient>(request);
        entity.Id = Guid.NewGuid();

        _context.ApplicationClient.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ApplicationClientDto>(entity);
    }
}