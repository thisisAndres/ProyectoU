using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WEB_UI.Models;

namespace WEB_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("VehiclesApi");
        }

        public async Task<IActionResult> Index()
        {
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
                VehiclesWithoutOwner = withoutOwner,
                RecentActivity = BuildRecentActivity(totalVehicles, totalPersons, withOwner, withoutOwner)
            };

            return View(model);
        }

        private List<ActivityEntryViewModel> BuildRecentActivity(
            int totalVehicles,
            int totalPersons,
            int withOwner,
            int withoutOwner)
        {
            var now = DateTime.Now;

            var list = new List<ActivityEntryViewModel>();

            // Estos son ejemplos; se ven bien para demo/presentación
            list.Add(new ActivityEntryViewModel
            {
                Timestamp = now.AddMinutes(-5),
                Title = "Asignación de propietario realizada",
                Description = "Se asignó un propietario a uno de los vehículos desde el módulo de Propietarios.",
                Category = "Propietarios",
                Level = "success"
            });

            list.Add(new ActivityEntryViewModel
            {
                Timestamp = now.AddMinutes(-25),
                Title = "Vehículo registrado",
                Description = "Se registró un nuevo vehículo en el sistema con sus datos básicos.",
                Category = "Vehículos",
                Level = "info"
            });

            list.Add(new ActivityEntryViewModel
            {
                Timestamp = now.AddHours(-1),
                Title = "Persona creada",
                Description = "Se añadió una nueva persona que ahora puede ser asignada como propietaria.",
                Category = "Personas",
                Level = "info"
            });

            if (withoutOwner > 0)
            {
                list.Add(new ActivityEntryViewModel
                {
                    Timestamp = now.AddHours(-2),
                    Title = "Vehículos sin propietario",
                    Description = $"Actualmente hay {withoutOwner} vehículo(s) sin propietario asignado.",
                    Category = "Propietarios",
                    Level = "warning"
                });
            }

            list.Add(new ActivityEntryViewModel
            {
                Timestamp = now.AddHours(-6),
                Title = "Sincronización con API",
                Description = "Los datos fueron actualizados usando los endpoints remotos de la API.",
                Category = "API",
                Level = "secondary"
            });

            // Orden descendente por fecha (más reciente primero)
            return list
                .OrderByDescending(e => e.Timestamp)
                .ToList();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
