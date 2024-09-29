using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Application.Helpers;

public static class UserHelper
{
    public static int? GetUserId(HttpContext httpContext)
    {
        var userIdClaim = httpContext?.User.Claims.FirstOrDefault(c => c.Type == "userId");
        if (userIdClaim == null)
        {
            return null;
        }
        return int.Parse(userIdClaim.Value);
    }
}
