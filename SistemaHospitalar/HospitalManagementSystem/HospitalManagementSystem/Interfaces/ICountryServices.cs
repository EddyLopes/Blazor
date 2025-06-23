using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Interfaces;

public interface ICountryServices
{
    Task<List<Country>> GetCountriesAsync();
    Task<Country?> GetCountryByIdAsync(int id);
    Task<Country> AddCountryAsync(Country country);
    Task<Country?> UpdateCountryAsync(Country country);
    Task<Country?> DeleteCountryAsync(int id);
}
