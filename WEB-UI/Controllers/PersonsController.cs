using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using WEB_UI.Models;

namespace WEB_UI.Controllers
{
    public class PersonsController : Controller
    {
        private readonly HttpClient _httpClient;

        public PersonsController(IHttpClientFactory httpClientFactory)
        {
            // REUSAMOS el cliente ya configurado en Program.cs
            _httpClient = httpClientFactory.CreateClient("VehiclesApi");
        }

        // GET: /Persons
        // Lista todas las personas (GET /api/Person)
        public async Task<IActionResult> Index()
        {
            var persons = await _httpClient
                .GetFromJsonAsync<List<PersonViewModel>>("api/Person");

            return View(persons ?? new List<PersonViewModel>());
        }

        // GET: /Persons/Create
        // Muestra el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Persons/Create
        // Crea una persona en el API (POST /api/Person)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel person)
        {
            if (!ModelState.IsValid)
                return View(person);

            // El API espera PersonCreateUpdateDto (FirstName, LastName, Email)
            var createDto = new
            {
                person.FirstName,
                person.LastName,
                person.Email
            };

            var response = await _httpClient.PostAsJsonAsync("api/Person", createDto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Error al crear la persona en la API.");
            return View(person);
        }

        // GET: /Persons/Edit/5
        // Carga una persona por Id (GET /api/Person/{id})
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _httpClient
                .GetFromJsonAsync<PersonViewModel>($"api/Person/{id}");

            if (person == null)
                return NotFound();

            return View(person);
        }

        // POST: /Persons/Edit/5
        // Actualiza persona (PUT /api/Person/{id})
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PersonViewModel person)
        {
            if (id != person.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(person);

            var updateDto = new
            {
                person.FirstName,
                person.LastName,
                person.Email
            };

            var response = await _httpClient
                .PutAsJsonAsync($"api/Person/{id}", updateDto);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError(string.Empty, "Error al actualizar la persona en la API.");
            return View(person);
        }
    }
}