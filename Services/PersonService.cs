using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.DTOs;
using Project.Models;
using Project.Repositories;

namespace Project.Services;

public class PersonService : IPersonService
{
    private readonly IRepository<Person> _personRepository;
    private readonly IRepository<City> _cityRepository;
    private readonly ApplicationDbContext _context;

    public PersonService(IRepository<Person> personRepository, IRepository<City> cityRepository, ApplicationDbContext context)
    {
        _personRepository = personRepository;
        _cityRepository = cityRepository;
        _context = context;
    }

    public async Task<PagedResult<PersonDto>> GetPersonsAsync(PagedRequest request)
    {
        var query = _context.Persons
            .Include(p => p.City)
                .ThenInclude(c => c.Province)
            .AsQueryable();

        var totalCount = await query.CountAsync();
        
        var persons = await query
            .Skip(request.Skip)
            .Take(request.PageSize)
            .Select(p => new PersonDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                CityId = p.CityId,
                CityName = p.City != null ? p.City.Name : string.Empty,
                ProvinceName = p.City != null && p.City.Province != null ? p.City.Province.Name : string.Empty,
                Gender = p.Gender.ToString()
            })
            .ToListAsync();

        return new PagedResult<PersonDto>
        {
            Data = persons,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    public async Task<PersonDto?> GetPersonByIdAsync(int id)
    {
        var person = await _context.Persons
            .Include(p => p.City)
                .ThenInclude(c => c.Province)
            .Where(p => p.Id == id)
            .Select(p => new PersonDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                CityId = p.CityId,
                CityName = p.City != null ? p.City.Name : string.Empty,
                ProvinceName = p.City != null && p.City.Province != null ? p.City.Province.Name : string.Empty,
                Gender = p.Gender.ToString()
            })
            .FirstOrDefaultAsync();

        return person;
    }

    public async Task<PersonDto> CreatePersonAsync(CreatePersonDto createPersonDto)
    {
        if (!await _cityRepository.ExistsAsync(createPersonDto.CityId))
        {
            throw new ArgumentException("Invalid CityId");
        }

        if (await PersonExistsAsync(createPersonDto.FirstName, createPersonDto.LastName))
        {
            throw new InvalidOperationException("A person with this first name and last name already exists.");
        }

        var person = new Person
        {
            FirstName = createPersonDto.FirstName,
            LastName = createPersonDto.LastName,
            CityId = createPersonDto.CityId,
            Gender = Enum.Parse<Gender>(createPersonDto.Gender)
        };

        await _personRepository.AddAsync(person);

        return await GetPersonByIdAsync(person.Id) ?? throw new InvalidOperationException("Failed to create person");
    }

    public async Task<PersonDto?> UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto)
    {
        var person = await _personRepository.GetByIdAsync(id);
        if (person == null)
        {
            return null;
        }

        if (!await _cityRepository.ExistsAsync(updatePersonDto.CityId))
        {
            throw new ArgumentException("Invalid CityId");
        }

        if (await _context.Persons.AnyAsync(p => p.Id != id && 
            p.FirstName.ToLower() == updatePersonDto.FirstName.ToLower() && 
            p.LastName.ToLower() == updatePersonDto.LastName.ToLower()))
        {
            throw new InvalidOperationException("A person with this first name and last name already exists.");
        }

        person.FirstName = updatePersonDto.FirstName;
        person.LastName = updatePersonDto.LastName;
        person.CityId = updatePersonDto.CityId;
        person.Gender = Enum.Parse<Gender>(updatePersonDto.Gender);

        await _personRepository.UpdateAsync(person);

        return await GetPersonByIdAsync(id);
    }

    public async Task<bool> DeletePersonAsync(int id)
    {
        if (!await _personRepository.ExistsAsync(id))
        {
            return false;
        }

        await _personRepository.DeleteAsync(id);
        return true;
    }

    public async Task<bool> PersonExistsAsync(int id)
    {
        return await _personRepository.ExistsAsync(id);
    }

    public async Task<bool> PersonExistsAsync(string firstName, string lastName)
    {
        return await _personRepository.ExistsAsync(p => 
            p.FirstName.ToLower() == firstName.ToLower() && 
            p.LastName.ToLower() == lastName.ToLower());
    }
}
