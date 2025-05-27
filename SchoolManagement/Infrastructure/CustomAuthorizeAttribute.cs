using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SchoolManagement.Infrastructure;

internal sealed class CustomAuthorizeAttribute : ActionFilterAttribute
{
    public string Roles { get; set; } = string.Empty;
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.User.Identity?.IsAuthenticated is false)
        {
            context.Result = new RedirectResult("/Login");
            return;
        }
        
        var roles = this.Roles.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (roles.Length == 0)
        {
            return;
        }
        
        var userRoles = context.HttpContext.User.FindAll(ClaimTypes.Role).ToList();
        if (userRoles.Count == 0)
        {
            context.Result = new RedirectResult("/Login");
            return;
        }
        
        var hasRole = userRoles.Any(role => roles.Contains(role.Value, StringComparer.InvariantCultureIgnoreCase));
        if (hasRole is false)
        {
            context.Result = new RedirectResult("/Login");
            return;
        }
        
        ;
        /*if (context.HttpContext.IsLoggedIn() is false)
        {
            context.Result = new RedirectResult("/Login");
        }*/
    }
}