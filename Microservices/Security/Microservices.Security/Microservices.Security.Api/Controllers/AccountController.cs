using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microservices.Security.Application.Identity.Commands;
using Microservices.Security.Application.Identity.Querys;
using SWO.Microservices.Dotnet.Shared.ApiExtensions;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Setup.API;
using System.Net;
using Microservices.Security.Domain.Entities;
using System.Security.Claims;

namespace Microservices.Security.Api;

[ApiController]
[Authorize]
public class AccountController : ApiController
{
    public AccountController(IMediator? mediator) : base(mediator)
    {
    }
        
    [HttpPost("registeruser")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResultDto<IdentityResult>), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> RegisterUser(CreateUser user)
    {
        return await Mediator.Send(user).ToActionResult<IdentityResult>();
    }

    [HttpPost("logininternaluser")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResultDto<TokenResult>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<TokenResult>), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> LoginUser(LoginInternalUser user)
    {
        return await Mediator.Send(user).ToActionResult();
    }

    [HttpDelete("deleteuser")]    
    [ProducesResponseType(typeof(ResultDto<IdentityResult>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> DeleteUser(DeleteUser user)
    {
        return await Mediator.Send(user).ToActionResult<IdentityResult>();
    }
    
    [HttpPost("createrole")]    
    [ProducesResponseType(typeof(ResultDto<IdentityResult>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> CreateRole(CreateRole role)
    {
        return await Mediator.Send(role).ToActionResult<IdentityResult>();
    }

    [HttpDelete("deleterole")]
    [ProducesResponseType(typeof(ResultDto<IdentityResult>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> DeleteRole(DeleteRole role)
    {
        return await Mediator.Send(role).ToActionResult<IdentityResult>();
    }

    [HttpPut("updaterole")]    
    [ProducesResponseType(typeof(ResultDto<IdentityResult>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateRole(UpdateRole role)
    {
        return await Mediator.Send(role).ToActionResult<IdentityResult>();
    }

    [HttpGet("getrolebyname/{rolename}")]
    [ProducesResponseType(typeof(ResultDto<ApplicationRole>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetRoleByName(string rolename)
    {
        return await Mediator.Send(new GetRoleByName { RoleName = rolename }).ToActionResult();
    }

    [HttpGet("getrolebyid/{id}")]
    [ProducesResponseType(typeof(ResultDto<ApplicationRole>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetRoleById(string id)
    {
        return await Mediator.Send(new GetRoleById { RoleId = id }).ToActionResult();
    }


    [HttpPost("adduserinrole")]
    [ProducesResponseType(typeof(ResultDto<IdentityResult>), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> AddUserInRole(AddUserToRole entity)
    {
        return await Mediator.Send(entity).ToActionResult<IdentityResult>();
    }

    [HttpPost("deleteuserfromrole")]
    [ProducesResponseType(typeof(ResultDto<IdentityResult>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> DeleteUserFromRole(DeleteUserFromRole entity)
    {
        return await Mediator.Send(entity).ToActionResult<IdentityResult>();
    }

    [HttpGet("validatetoken")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResultDto<ClaimsPrincipal>), (int)HttpStatusCode.Accepted)]
    [ProducesResponseType(typeof(ResultDto<ProblemDetails>), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> ValidateToken([FromQuery] ValidateToken entity)
    {
        return await Mediator.Send(entity).ToActionResult();
    }
}