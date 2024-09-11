using Microservices.Security.Domain.Entities;
using Microservices.Security.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using SWO.Microservices.Dotnet.Shared.Domain.Entities;

namespace Microservices.Security.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<TokenResult> LoginInternalUser(string userName, string password);

    Task<IdentityResult> CreateUserAsync(string userName, string password, string loginProvider);

    Task<IdentityResult> DeleteUserAsync(string userId);

    Task<ApplicationUser?> FindUserByIdAsync(string userId);

    Task<ApplicationUser?> FindUserByNameAsync(string normalizedUserName);

    Task<ApplicationUser?> FindUserByLoginAsync(string loginProvider, string providerKey);

    Task<IEnumerable<UserLoginInfo>> GetLoginsAsync(string userId);

    Task<IdentityResult> AddUserToRoleAsync(string userName, string roleName);

    Task<IdentityResult> RemoveUserFromRoleAsync(string userName, string roleName);

    Task<IEnumerable<string>> GetUserRolesAsync(string userName);

    Task<IEnumerable<ApplicationUser>> GetUsersInRoleAsync(string roleName);

    Task<bool> IsUserInRoleAsync(string userName, string roleName);

    Task<bool> AuthorizeAsync(string userId, string policyName);


    Task<IdentityResult> CreateRoleAsync(ApplicationRole rolen);

    Task<IdentityResult> DeleteRoleAsync(string roleId);

    Task<IdentityResult> UpdateRoleAsync(ApplicationRole role);

    Task<ApplicationRole?> FindRoleByIdAsync(string roleId);

    Task<ApplicationRole?> FindRoleByNameAsync(string RoleName);
}
