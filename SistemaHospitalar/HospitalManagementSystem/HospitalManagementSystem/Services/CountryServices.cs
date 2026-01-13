using HospitalManagementSystem.Data;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;

public class CountryServices : ICountryServices
{
    private readonly ApplicationDbContext _context;

    public CountryServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Country> AddCountryAsync(Country country)
    {
        country.CreatedOn = DateTime.UtcNow;
        await _context.Countries.AddAsync(country);
        await _context.SaveChangesAsync();
        return country;
    }

    public async Task<Country?> UpdateCountryAsync(Country country)
    {
        var existingCountry = await _context.Countries.FirstOrDefaultAsync(x => x.Id == country.Id);

        if (existingCountry is null)
            return null;

        existingCountry.Name = country.Name;
        existingCountry.Code = country.Code;
        existingCountry.ModifiedOn = DateTime.UtcNow;
        existingCountry.ModifiedById = country.ModifiedById;

        _context.Countries.Update(existingCountry);
        await _context.SaveChangesAsync();
        return existingCountry;
    }

    public async Task<Country?> DeleteCountryAsync(int id)
    {
        var existingCountry = await _context.Countries.FindAsync(id);
        if (existingCountry == null)
            return null;

        _context.Countries.Remove(existingCountry);
        await _context.SaveChangesAsync();
        return existingCountry;
    }

    public async Task<Country?> GetCountryByIdAsync(int id)
    {
        return await _context.Countries
                             .Include(x => x.CreatedBy)
                             .Include(x => x.ModifiedBy)
                             .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Country>> GetCountriesAsync()
    {
        return await _context.Countries
                             .Include(x => x.CreatedBy)
                             .Include(x => x.ModifiedBy)
                             .ToListAsync();
    }

}
