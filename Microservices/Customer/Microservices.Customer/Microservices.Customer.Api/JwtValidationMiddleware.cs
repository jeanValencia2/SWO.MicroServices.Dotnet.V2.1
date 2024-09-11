using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Models;
using SWO.Microservices.Dotnet.Shared.ApiExtensions.Result;

namespace Microservices.Customer.Api;

public class JwtValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly HttpClient _httpClient;

    public JwtValidationMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
    {
        _next = next;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        string path = context.Request.Path;
        bool isValid = false;

        if (path.Contains("/api/v1"))
        {
            string? authorizationHeader = context.Request.Headers["Authorization"];
            var authorizeAttribute = (endpoint?.Metadata?.GetMetadata<AuthorizeAttribute>() != null);


            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid autentication");
                return;
            }

            string jwtToken = authorizationHeader.Substring("Bearer ".Length).Trim();
            HttpResponseMessage response = await _httpClient.GetAsync("https://localhost:44391/api/v1/account/validatetoken?Token=" + jwtToken);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResultDto<bool>>(responseBody);
                isValid = result.Value;
            }

            if (!isValid)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid Token");
                return;
            }
        }

        await _next(context);
    }
}
