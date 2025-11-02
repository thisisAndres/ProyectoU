using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Dtos;
using API.Models;

using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController(AppDbContext db) : ControllerBase
    {
        // GET: api/person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonReadDto>>> GetAll()
        {
            var data = await db.Persons
                .Select(p => new PersonReadDto { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Email = p.Email })
                .ToListAsync();
            return Ok(data);
        }

        // POST: api/person
        [HttpPost]
        public async Task<ActionResult<PersonReadDto>> Create(PersonCreateUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName))
                return BadRequest("FirstName is required.");

            if (string.IsNullOrWhiteSpace(dto.LastName))
                return BadRequest("LastName is required.");

            var entity = new Person { FirstName = dto.FirstName, LastName = dto.LastName,  Email = dto.Email };
            db.Persons.Add(entity);
            await db.SaveChangesAsync();

            var read = new PersonReadDto { Id = entity.Id, FirstName = dto.FirstName, LastName = dto.LastName, Email = entity.Email };
            return CreatedAtAction(nameof(GetById), new { id = read.Id }, read);
        }

        // GET: api/persons/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonReadDto>> GetById(int id)
        {
            var p = await db.Persons.FindAsync(id);
            if (p is null) return NotFound();
            return new PersonReadDto { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, Email = p.Email };
        }
        // PUT: api/persons/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, PersonCreateUpdateDto dto)
        {
            var p = await db.Persons.FindAsync(id);
            if (p is null) return NotFound();

            p.FirstName = dto.FirstName;
            p.LastName = dto.LastName;
            p.Email = dto.Email;
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
