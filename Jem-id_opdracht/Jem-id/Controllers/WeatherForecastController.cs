using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jem_id.webapi.Models;


namespace Jem_id.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly PlantContext _context;

        public PlantController(PlantContext context)
        {
            _context = context;
        }

        // GET: api/Plant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants([FromQuery] string naam, [FromQuery] int? potmaatMin, [FromQuery] int? potmaatMax, [FromQuery] string kleur, [FromQuery] string productgroep, [FromQuery] string sortBy, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _context.Artikel.AsQueryable();

            if (!string.IsNullOrEmpty(naam))
                query = query.Where(p => p.Naam.Contains(naam));

            if (potmaatMin.HasValue)
                query = query.Where(p => p.Potmaat >= potmaatMin.Value);

            if (potmaatMax.HasValue)
                query = query.Where(p => p.Potmaat <= potmaatMax.Value);

            if (!string.IsNullOrEmpty(kleur))
                query = query.Where(p => p.Kleur == kleur);

            if (!string.IsNullOrEmpty(productgroep))
                query = query.Where(p => p.Productgroep == productgroep);

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy switch
                {
                    "naam" => query.OrderBy(p => p.Naam),
                    "potmaat" => query.OrderBy(p => p.Potmaat),
                    "planthoogte" => query.OrderBy(p => p.Pothoogte),
                    _ => query.OrderBy(p => p.Code)
                };
            }

            var pagedData = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(pagedData);
        }

        // GET: api/Plant/5
        [HttpGet("{code}")]
        public async Task<ActionResult<Plant>> GetPlant(string code)
        {
            var plant = await _context.Artikel.FindAsync(code);

            if (plant == null)
            {
                return NotFound();
            }

            return plant;
        }

        // POST: api/Plant
        [HttpPost]
        public async Task<ActionResult<Plant>> PostPlant(Plant plant)
        {
            _context.Artikel.Add(plant);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlant), new { code = plant.Code }, plant);
        }

        // PUT: api/Plant/5
        [HttpPut("{code}")]
        public async Task<IActionResult> PutPlant(string code, Plant plant)
        {
            if (code != plant.Code)
            {
                return BadRequest();
            }

            _context.Entry(plant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(code))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Plant/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> DeletePlant(string code)
        {
            var plant = await _context.Artikel.FindAsync(code);
            if (plant == null)
            {
                return NotFound();
            }

            _context.Artikel.Remove(plant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantExists(string code)
        {
            return _context.Artikel.Any(e => e.Code == code);
        }
    }
}
