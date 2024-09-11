using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microservices.Security.Application.Common.Interfaces;
using Microservices.Security.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;
using SWO.Microservices.Dotnet.Shared.Setup.Services;

namespace Microservices.Security.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;    
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;    
    private readonly IServiceToken _serviceToken;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,        
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,        
        IServiceToken serviceToken)
    {
        _userManager = userManager;
        _roleManager = roleManager;        
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;        
        _serviceToken = serviceToken;

    }

    public async Task<TokenResult> LoginInternalUser(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
            throw new Exception("Invalid user or password");

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
            throw new Exception("Invalid user or password");
        
        var roles = await GetUserRolesAsync(userName);
        var resultAccessToken = _serviceToken.GenerateAccessToken(user, roles.ToList());
        var resultRefreshToken = _serviceToken.GenerateRefreshToken(user);

        user.RefreshToken = resultRefreshToken.Result.refreshToken;
        user.RefreshTokenExpiryTime = resultRefreshToken.Result.refreshTokenExpiryTime;
        await _userManager.UpdateAsync(user);

        var tokenResult = new TokenResult
        {
            Type = "Bearer",
            AccessToken = resultAccessToken.Result.accessToken,
            RefreshToken = resultRefreshToken.Result.refreshToken,
            ExpireIn = resultAccessToken.Result.expireIn
        };

        return tokenResult;
    }

    public async Task<IdentityResult> CreateUserAsync(string userName, string password, string loginProvider)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };
        var userLogin = new UserLoginInfo(loginProvider, Guid.NewGuid().ToString(), "");

        await _userManager.CreateAsync(user, password);

        return await _userManager.AddLoginAsync(user, userLogin);
    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
        var result = user != null ? await _userManager.DeleteAsync(user) : IdentityResult.Failed();

        return result!;
    }

    public async Task<ApplicationUser?> FindUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<ApplicationUser?> FindUserByNameAsync(string normalizedUserName)
    {
        return await _userManager.FindByNameAsync(normalizedUserName);
    }

    public async Task<ApplicationUser?> FindUserByLoginAsync(string loginProvider, string providerKey)
    {
        return await _userManager.FindByLoginAsync(loginProvider, providerKey);
    }

    public async Task<IEnumerable<UserLoginInfo>> GetLoginsAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);
        return await _userManager.GetLoginsAsync(user!);
    }

    public async Task<IdentityResult> AddUserToRoleAsync(string userName, string roleName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);
        var role = _roleManager.Roles.SingleOrDefault(r => r.Name == roleName);

        if (user != null && role != null)
            return await _userManager.AddToRoleAsync(user, role.Name!);
        else
            return IdentityResult.Failed();
    }

    public async Task<IdentityResult> RemoveUserFromRoleAsync(string userName, string roleName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);
        var role = _roleManager.Roles.SingleOrDefault(r => r.Name == roleName);

        if (user != null && role != null)
            return await _userManager.RemoveFromRoleAsync(user, role.Name!);
        else
            return IdentityResult.Failed();
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(string userName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

        return await _userManager.GetRolesAsync(user!);
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName)
    {
        var role = _roleManager.Roles.SingleOrDefault(r => r.Name == roleName);

        if (role != null)
            return await _userManager.GetUsersInRoleAsync(role.Name!);
        else
            return new List<ApplicationUser>();
    }

    public async Task<bool> IsUserInRoleAsync(string userName, string roleName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.UserName == userName);

        return user != null && await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<IdentityResult> CreateRoleAsync(ApplicationRole role)
    {
         return await _roleManager.CreateAsync(role);
    }

    public async Task<IdentityResult> DeleteRoleAsync(string roleId)
    {
        var role = _roleManager.Roles.SingleOrDefault(r => r.Id == roleId);

        if (role != null)
            return await _roleManager.DeleteAsync(role);
        else
            return IdentityResult.Failed();
    }

    public async Task<IdentityResult> UpdateRoleAsync(ApplicationRole role)
    {
        var roleResult = await _roleManager.FindByIdAsync(role.Id);

        if (roleResult == null)
            return IdentityResult.Failed();

        roleResult.Name = role.Name;
        roleResult.Description = role.Description;
        roleResult.ApplicationId = role.ApplicationId;

        return await _roleManager.UpdateAsync(roleResult);
    }

    public async Task<ApplicationRole?> FindRoleByIdAsync(string roleId)
    {
        return await _roleManager.FindByIdAsync(roleId);
    }

    public async Task<ApplicationRole?> FindRoleByNameAsync(string RoleName)
    {
        return await _roleManager.FindByNameAsync(RoleName);
    }
}
