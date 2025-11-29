using Microsoft.AspNetCore.Mvc;
using WEB_UI.Models;

namespace WEB_UI.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly HttpClient _httpClient;

        public VehiclesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("VehiclesApi");
        }

        // LISTAR VEHÍCULOS (GET /api/Vehicles -> List<VehicleReadDto>)
        public async Task<IActionResult> Index()
        {
            var vehicles = await _httpClient
                .GetFromJsonAsync<List<VehicleViewModel>>("api/Vehicles");
            return View(vehicles ?? new List<VehicleViewModel>());
        }
        // GET: Crear vehículo
        public IActionResult Create()
        {
            return View();
        }
        // POST: Crear vehículo (POST /api/Vehicles con VehicleCreateUpdateDto)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel vehicle)
        {
            if (!ModelState.IsValid)
                return View(vehicle);

            var createDto = new
            {
                vehicle.Plate,
                vehicle.Brand,
                vehicle.Model,
                vehicle.OwnerId
            };

            var response = await _httpClient.PostAsJsonAsync("api/Vehicles", createDto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Error al crear el vehículo en la API.");
            return View(vehicle);
        }

        // GET: Editar vehículo (GET /api/Vehicles/{id} -> VehicleReadDto)
        public async Task<IActionResult> Edit(int id)
        {
            var vehicle = await _httpClient
                .GetFromJsonAsync<VehicleViewModel>($"api/Vehicles/{id}");

            if (vehicle == null)
                return NotFound();

            return View(vehicle);
        }

        // POST: Editar vehículo (PUT /api/Vehicles/{id} con VehicleCreateUpdateDto)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleViewModel vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(vehicle);

            var updateDto = new
            {
                vehicle.Plate,
                vehicle.Brand,
                vehicle.Model,
                vehicle.OwnerId
            };

            var response = await _httpClient
                .PutAsJsonAsync($"api/Vehicles/{id}", updateDto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Error al actualizar el vehículo en la API.");
            return View(vehicle);
        }

    }
}
