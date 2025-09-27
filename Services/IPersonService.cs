using Project.DTOs;

namespace Project.Services;

public interface IPersonService
{
    Task<PagedResult<PersonDto>> GetPersonsAsync(PagedRequest request);
    Task<PersonDto?> GetPersonByIdAsync(int id);
    Task<PersonDto> CreatePersonAsync(CreatePersonDto createPersonDto);
    Task<PersonDto?> UpdatePersonAsync(int id, UpdatePersonDto updatePersonDto);
    Task<bool> DeletePersonAsync(int id);
    Task<bool> PersonExistsAsync(int id);
    Task<bool> PersonExistsAsync(string firstName, string lastName);
}
