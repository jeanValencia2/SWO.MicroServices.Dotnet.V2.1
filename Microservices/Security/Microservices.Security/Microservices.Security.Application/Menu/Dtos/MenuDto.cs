﻿using AutoMapper;
using Microservices.Security.Application.Menu.Commands;
using SWO.Microservices.Dotnet.Shared.Domain;

namespace Microservices.Security.Domain.Entities;

public class MenuDto : BaseAuditableEntity<Guid>
{
    public string? Path { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Class { get; set; }
    public string? Badge { get; set;}
    public string? BadgeClass { get; set;}
    public Boolean  IsExternalLink { get;}
    public Boolean IsParent { get;}    
    public Boolean  IsLocked { get; set; }
    public Guid? MenuId { get; set; }
    public Guid ApplicationId { get; set;}
    public List<string>? Roles { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Menu, MenuDto>();
            CreateMap<CreateMenu, Menu>();
        }
    }
}
