using Demo16Nov.Web.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo16Nov.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public HomeController(IHttpClientFactory httpClientFactory) =>
        _httpClientFactory = httpClientFactory;

    public async Task<IActionResult> Index()
    {
        using var identityClient = _httpClientFactory.CreateClient();
        var disco = await identityClient.GetDiscoveryDocumentAsync("http://localhost:5100");
        if (disco.IsError) return Content("Error");
        var tokenResponse = await identityClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "Demo16NovWeb",
            ClientSecret = "secret",
            Scope = "Demo16NovApi"
        });
        if (tokenResponse.IsError || tokenResponse.AccessToken is null) return Content("Error");

        using var weatherClient = _httpClientFactory.CreateClient();
        weatherClient.SetBearerToken(tokenResponse.AccessToken);
        var weatherForecasts = await weatherClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("http://localhost:5148/weatherforecast");
        return View(weatherForecasts);
    }

    [HttpPost]
    public async Task<IActionResult> IndexPost()
    {
        using var identityClient = _httpClientFactory.CreateClient();
        var disco = await identityClient.GetDiscoveryDocumentAsync("http://localhost:5100");
        if (disco.IsError) return Content("Error");
        var tokenResponse = await identityClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "Demo16NovWeb",
            ClientSecret = "secret",
            Scope = "Demo16NovApi"
        });
        if (tokenResponse.IsError || tokenResponse.AccessToken is null) return Content("Error");

        using var weatherClient = _httpClientFactory.CreateClient();
        weatherClient.SetBearerToken(tokenResponse.AccessToken);
        var res = await weatherClient.PostAsync("http://localhost:5148/weatherforecast", default);
        return Content("Success");
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