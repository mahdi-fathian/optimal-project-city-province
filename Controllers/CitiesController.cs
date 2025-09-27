using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly IApplicationDbContext _context;

    public CitiesController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        return await _context.Cities
            .Include(c => c.Province)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(int id)
    {
        var city = await _context.Cities
            .Include(c => c.Province)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (city == null)
        {
            return NotFound();
        }

        return city;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCity(int id, City city)
    {
        if (id != city.Id)
        {
            return BadRequest();
        }

        _context.Entry(city).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CityExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<City>> PostCity(City city)
    {
        if (!_context.Provinces.Any(p => p.Id == city.ProvinceId))
        {
            return BadRequest("Invalid ProvinceId");
        }

        if (_context.Cities.Any(c => c.Name.ToLower() == city.Name.ToLower() && c.ProvinceId == city.ProvinceId))
        {
            return BadRequest("A city with this name already exists in the specified province.");
        }

        city.Id = 0;
        _context.Cities.Add(city);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city == null)
        {
            return NotFound();
        }

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CityExists(int id)
    {
        return _context.Cities.Any(e => e.Id == id);
    }
}
