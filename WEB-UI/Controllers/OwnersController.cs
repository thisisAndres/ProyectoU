using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;
using WEB_UI.Models;

namespace WEB_UI.Controllers
{
    public class OwnersController : Controller
    {
        private readonly HttpClient _httpClient;

        public OwnersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("VehiclesApi");
        }

        // GET: /Owners
        // Llama a GET /api/Vehicles/with-owner y muestra la tabla
        public async Task<IActionResult> Index()
        {
            var vehicles = await _httpClient
                .GetFromJsonAsync<List<VehicleWithOwnerViewModel>>("api/Vehicles/with-owner");

            return View(vehicles ?? new List<VehicleWithOwnerViewModel>());
        }

        // GET: /Owners/Edit/{vehicleId}
        // Prepara el formulario para asignar/cambiar propietario de un vehículo
        public async Task<IActionResult> Edit(int id)
        {
            // 1) Obtenemos todos los vehículos con propietario
            var vehicles = await _httpClient
                .GetFromJsonAsync<List<VehicleWithOwnerViewModel>>("api/Vehicles/with-owner");

            var vehicle = vehicles?.FirstOrDefault(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            // 2) Obtenemos la lista de Personas para el combo de propietarios
            var persons = await _httpClient
                .GetFromJsonAsync<List<PersonViewModel>>("api/Person");

            var model = new AssignOwnerViewModel
            {
                VehicleId = vehicle.Id,
                Plate = vehicle.Plate,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                OwnerId = vehicle.OwnerId,
                AvailableOwners = persons?
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = $"{p.FirstName} {p.LastName}"
                    })
                    .ToList() ?? new List<SelectListItem>()
            };

            return View(model);
        }

        // POST: /Owners/Edit/{vehicleId}
        // Llama a PUT /api/Vehicles/{vehicleId}/Owner/{ownerId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AssignOwnerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Si falla la validación, recargamos la lista de propietarios
                var persons = await _httpClient
                    .GetFromJsonAsync<List<PersonViewModel>>("api/Person");

                model.AvailableOwners = persons?
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = $"{p.FirstName} {p.LastName}"
                    })
                    .ToList() ?? new List<SelectListItem>();

                return View(model);
            }

            if (model.OwnerId == null)
            {
                ModelState.AddModelError(string.Empty, "Debe seleccionar un propietario.");
                return View(model);
            }

            // El endpoint es:
            // PUT /api/Vehicles/{vehicleId}/Owner/{ownerId}
            var requestUri = $"api/Vehicles/{model.VehicleId}/Owner/{model.OwnerId}";

            var response = await _httpClient.PutAsync(requestUri, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Error al asignar el propietario en la API.");

            // Recargamos la lista de propietarios en caso de error
            var personsAgain = await _httpClient
                .GetFromJsonAsync<List<PersonViewModel>>("api/Person");

            model.AvailableOwners = personsAgain?
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.FirstName} {p.LastName}"
                })
                .ToList() ?? new List<SelectListItem>();

            return View(model);
        }
    }
}
