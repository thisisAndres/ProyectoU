
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Json;
using WEB_UI.Models;

namespace WEB_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("VehiclesApi");
        }

        public async Task<IActionResult> Index()
        {
            // Llamadas reales a la API desde el backend (SIN JS)
            var vehicles = await _httpClient
                .GetFromJsonAsync<List<object>>("api/Vehicles") ?? new();

            var persons = await _httpClient
                .GetFromJsonAsync<List<object>>("api/Person") ?? new();

            var vehiclesWithOwner = await _httpClient
                .GetFromJsonAsync<List<VehicleWithOwnerViewModel>>("api/Vehicles/with-owner") ?? new();

            int totalVehicles = vehicles.Count;
            int totalPersons = persons.Count;

            int withOwner = vehiclesWithOwner.Count(v => v.OwnerId != null);
            int withoutOwner = vehiclesWithOwner.Count(v => v.OwnerId == null);

            var model = new DashboardViewModel
            {
                TotalVehicles = totalVehicles,
                TotalPersons = totalPersons,
                VehiclesWithOwner = withOwner,
                VehiclesWithoutOwner = withoutOwner
            };

            return View(model);
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
}
