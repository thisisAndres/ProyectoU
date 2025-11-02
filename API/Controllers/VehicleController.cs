using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Dtos;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController(AppDbContext db) : ControllerBase
    {
        // GET: api/vehicles  (lista simple)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleReadDto>>> GetAll()
        {
            var data = await db.Vehicles
                .Select(v => new VehicleReadDto
                {
                    Id = v.Id,
                    Plate = v.Plate,
                    Brand = v.Brand,
                    Model = v.Model,
                    OwnerId = v.OwnerId
                }).ToListAsync();
            return Ok(data);
        }

        // GET: api/vehicles/with-owner  (lista con dueño asociado: Placa, Modelo, Marca, Dueño Actual)
        [HttpGet("with-owner")]
        public async Task<ActionResult<IEnumerable<VehicleWithOwnerDto>>> GetAllWithOwner()
        {
            var data = await db.Vehicles
                .Include(v => v.Owner)
                .Select(v => new VehicleWithOwnerDto
                {
                    Id = v.Id,
                    Plate = v.Plate,
                    Brand = v.Brand,
                    Model = v.Model,
                    OwnerId = v.OwnerId,
                    OwnerName = v.Owner != null ? v.Owner.FirstName + " " + v.Owner.LastName : null
                }).ToListAsync();

            return Ok(data);
        }

        // POST: api/vehicles  (agregar vehículo; opcionalmente con OwnerId)
        [HttpPost]
        public async Task<ActionResult<VehicleReadDto>> Create(VehicleCreateUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Plate))
                return BadRequest("Plate is required.");

            // Si envían OwnerId, debe existir
            if (dto.OwnerId.HasValue && !await db.Persons.AnyAsync(p => p.Id == dto.OwnerId))
                return BadRequest("OwnerId not found.");

            var entity = new Vehicle
            {
                Plate = dto.Plate.Trim().ToUpperInvariant(),
                Brand = dto.Brand,
                Model = dto.Model,
                OwnerId = dto.OwnerId
            };

            db.Vehicles.Add(entity);
            await db.SaveChangesAsync();

            var read = new VehicleReadDto
            {
                Id = entity.Id,
                Plate = entity.Plate,
                Brand = entity.Brand,
                Model = entity.Model,
                OwnerId = entity.OwnerId
            };
            return CreatedAtAction(nameof(GetById), new { id = read.Id }, read);
        }

        // GET: api/vehicles/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VehicleReadDto>> GetById(int id)
        {
            var v = await db.Vehicles.FindAsync(id);
            if (v is null) return NotFound();

            return new VehicleReadDto
            {
                Id = v.Id,
                Plate = v.Plate,
                Brand = v.Brand,
                Model = v.Model,
                OwnerId = v.OwnerId
            };
        }

        // PUT: api/vehicles/5  (editar vehículo)
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, VehicleCreateUpdateDto dto)
        {
            var v = await db.Vehicles.FindAsync(id);
            if (v is null) return NotFound();

            if (dto.OwnerId.HasValue && !await db.Persons.AnyAsync(p => p.Id == dto.OwnerId))
                return BadRequest("OwnerId not found.");

            v.Plate = dto.Plate.Trim().ToUpperInvariant();
            v.Brand = dto.Brand;
            v.Model = dto.Model;
            v.OwnerId = dto.OwnerId;

            await db.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/vehicles/5/owner/3  (Seleccionar/editar dueño actual desde la lista)
        [HttpPut("{vehicleId:int}/owner/{ownerId:int}")]
        public async Task<IActionResult> SetOwner(int vehicleId, int ownerId)
        {
            var v = await db.Vehicles.FindAsync(vehicleId);
            if (v is null) return NotFound("Vehicle not found.");

            var p = await db.Persons.FindAsync(ownerId);
            if (p is null) return NotFound("Person (owner) not found.");

            v.OwnerId = ownerId;
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
