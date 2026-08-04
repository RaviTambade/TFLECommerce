using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceApplication.Filters;

public class AdminOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var http = context.HttpContext;
        string? email = http.Session.GetString("Email");
        string? role = http.Session.GetString("Role");

        if (string.IsNullOrWhiteSpace(email))
        {
            context.Result = new RedirectToActionResult("Login", "Authentication", null);
            return;
        }

        if (!string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new StatusCodeResult(403);
            return;
        }

        base.OnActionExecuting(context);
    }
}

