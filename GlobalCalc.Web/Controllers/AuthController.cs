using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace GlobalCalc.Web.Controllers;

public class AuthorizeAttribute : Attribute { }

public class AuthController : Controller
{
    protected virtual IActionResult OnAuthenticateFailed() => StatusCode(403);

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = (Controller)context.Controller;
        bool hasAuth = controller.ControllerContext.ActionDescriptor
            .MethodInfo.GetCustomAttribute<AuthorizeAttribute>() != null;
        if (!hasAuth) return;

        if (!controller.Request.Cookies.TryGetValue("access_token", out string? accessToken))
            context.Result = OnAuthenticateFailed();

        var config = controller.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        if (accessToken != config["Authentication:AdminToken"])
            context.Result = OnAuthenticateFailed();
    }
}
