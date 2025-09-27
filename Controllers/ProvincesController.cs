using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvincesController : ControllerBase
{
    private readonly IApplicationDbContext _context;

    public ProvincesController(IApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Province>>> GetProvinces()
    {
        return await _context.Provinces.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Province>> GetProvince(int id)
    {
        var province = await _context.Provinces.FindAsync(id);

        if (province == null)
        {
            return NotFound();
        }

        return province;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProvince(int id, Province province)
    {
        if (id != province.Id)
        {
            return BadRequest();
        }

        _context.Entry(province).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProvinceExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Province>> PostProvince(Province province)
    {
        if (_context.Provinces.Any(p => p.Name.ToLower() == province.Name.ToLower()))
        {
            return BadRequest("A province with this name already exists.");
        }

        province.Id = 0;
        foreach (var city in province.Cities)
        {
            city.Id = 0;
        }
        _context.Provinces.Add(province);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProvince), new { id = province.Id }, province);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProvince(int id)
    {
        var province = await _context.Provinces.FindAsync(id);
        if (province == null)
        {
            return NotFound();
        }

        _context.Provinces.Remove(province);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProvinceExists(int id)
    {
        return _context.Provinces.Any(e => e.Id == id);
    }
}
