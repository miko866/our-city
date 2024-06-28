using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Server.Config.SwaggerConfigs;

public class SwaggerBasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public SwaggerBasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            string? authHeader = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic "))
            {
                // Get the credentials from request header
                AuthenticationHeaderValue header = AuthenticationHeaderValue.Parse(authHeader);

                if (header.Parameter != null)
                {
                    byte[] inBytes = Convert.FromBase64String(header.Parameter);
                    string[] credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                    string username = credentials[0];
                    string password = credentials[1];

                    // Check credentials
                    if (
                        username.Equals(_configuration.GetSection("SwaggerAuth:Username").Value)
                        && password.Equals(_configuration.GetSection("SwaggerAuth:Password").Value)
                    )
                    {
                        await _next.Invoke(context).ConfigureAwait(false);
                        return;
                    }
                }
            }

            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            await _next.Invoke(context).ConfigureAwait(false);
        }
    }
}
