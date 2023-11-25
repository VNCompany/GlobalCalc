using Microsoft.AspNetCore.Mvc;

using GlobalCalc.Web.Services;

namespace GlobalCalc.Web.Controllers;

public class AdminController : Controller
{
    private readonly IConfiguration _config;
    private readonly AuthorizationService _auth;

    public AdminController(IConfiguration config, AuthorizationService auth)
    {
        _config = config;
        _auth = auth;
    }

    public IActionResult Index()
    {
        if (!_auth.Authenticate(Request.Cookies)) return OnAuthenticateFailed();

        return Ok("Main page");
    }

    public string Hi() => "Hello world!";

    public IActionResult Login(string? login, string? password)
    {
        if (_auth.Authenticate(Request.Cookies)) return RedirectToAction(nameof(Index));

        if (login == null || password == null)
            return View();

        if (!_auth.Login(login, password, Response.Cookies))
        {
            ViewData["Login"] = false;
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    protected IActionResult OnAuthenticateFailed() => RedirectToAction(nameof(Login));
}
