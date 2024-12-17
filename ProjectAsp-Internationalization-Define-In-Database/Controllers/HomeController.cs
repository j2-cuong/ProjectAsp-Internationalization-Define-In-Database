using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MultiLanguageApp.Models;

namespace MultiLanguageApp.Controllers;

public class HomeController : Controller
{
    private readonly ICookiesRepository _cookiesRepository;
    private readonly IDatabaseLocalizer _databaseLocalizer;

    public HomeController(ICookiesRepository cookiesRepository, IDatabaseLocalizer databaseLocalizer)
    {
        _cookiesRepository = cookiesRepository;
        _databaseLocalizer = databaseLocalizer;
    }

    public async Task<IActionResult> ChangeLanguage(string culture, string returnUrl)
    {
        _cookiesRepository.SetLanguage(culture);
        return LocalRedirect(returnUrl);
    }

    public async Task<IActionResult> GetLanguageConfig()
    {
        var listLanguage = await _databaseLocalizer.GetLanguageConfig();
        return Json(listLanguage);
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
