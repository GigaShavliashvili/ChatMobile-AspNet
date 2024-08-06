using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace chatmobile.MiddleWares
{


    public class AuthMiddlewate
    {

        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuthMiddlewate(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Path.Value.Equals("/api/v1/login"))
            {

                await _next.Invoke(context);
            }
            var accessToken = context.Request.Headers["Authorization"];

            if (accessToken.Count > 0 && accessToken[0].StartsWith("Bearer "))
            {
                accessToken = accessToken[0].Substring("Bearer ".Length);

                var principal = GetPrincipalFromExpiredToken(accessToken);
                if (principal == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? "")),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                //todo unhandling error occured
                throw new Exception("invalid Token");
            }
            return principal;
        }
    }
}

