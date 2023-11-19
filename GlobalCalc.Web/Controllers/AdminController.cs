using Microsoft.AspNetCore.Mvc;

using GlobalCalc.Web.Infrastructure;

namespace GlobalCalc.Web.Controllers;

public class AdminController : AuthController
{
    private readonly IConfiguration _config;
    public AdminController(IConfiguration config)
    {
        _config = config;
    }

    [Authorize]
    public IActionResult Index()
    {
        return Ok("Main page");
    }

    public IActionResult Login(string? login, string? password)
    {
        if (login == null || password == null)
            return View();

        string expectedHash = _config["Authentication:AdminToken"];
        string hash = HashTools.GenerateToken(login, password);
        if (expectedHash != hash)
        {
            ViewData["Login"] = false;
        }
        else
        {
            Response.Cookies.Append("access_token", hash, new CookieOptions { Expires = DateTime.UtcNow.AddDays(1) });
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

    protected override IActionResult OnAuthenticateFailed()
    {
        return RedirectToAction(nameof(Login));
    }
}
