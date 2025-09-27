using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonsController> _logger;

    public PersonsController(IPersonService personService, ILogger<PersonsController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<PersonDto>>> GetPersons([FromQuery] PagedRequest request)
    {
        try
        {
            var result = await _personService.GetPersonsAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting persons");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PersonDto>> GetPerson(int id)
    {
        try
        {
            var person = await _personService.GetPersonByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting person with id {PersonId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(int id, UpdatePersonDto updatePersonDto)
    {
        try
        {
            if (id != updatePersonDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _personService.UpdatePersonAsync(id, updatePersonDto);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating person with id {PersonId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpPost]
    public async Task<ActionResult<PersonDto>> PostPerson(CreatePersonDto createPersonDto)
    {
        try
        {
            var person = await _personService.CreatePersonAsync(createPersonDto);
            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating person");
            return StatusCode(500, "An error occurred while processing your request");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        try
        {
            var result = await _personService.DeletePersonAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting person with id {PersonId}", id);
            return StatusCode(500, "An error occurred while processing your request");
        }
    }
}
