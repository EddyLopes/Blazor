using HospitalManagementSystem.Data;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;

public class DepartamentServices : IDepartamentServices
{
    private readonly ApplicationDbContext _context;

    public DepartamentServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Departament> AddDepartamentAsync(Departament departament)
    {
        departament.CreatedOn = DateTime.UtcNow;
        await _context.Departaments.AddAsync(departament);
        await _context.SaveChangesAsync();
        return departament;
    }

    public async Task<Departament?> UpdateDepartamentAsync(Departament departament)
    {
        var existingDepartament = await _context.Departaments.FirstOrDefaultAsync(x => x.Id == departament.Id);

        if (existingDepartament is null)
            return null;

        existingDepartament.Code = departament.Code;
        existingDepartament.Name = departament.Name;
        existingDepartament.ModifiedOn = DateTime.UtcNow;
        existingDepartament.ModifiedById = departament.ModifiedById;

        _context.Departaments.Update(existingDepartament);
        await _context.SaveChangesAsync();
        return existingDepartament;
    }

    public async Task<Departament?> DeleteDepartamentAsync(int id)
    {
        var existingDepartament = await _context.Departaments.FindAsync(id);
        if (existingDepartament is null)
            return null;

        _context.Departaments.Remove(existingDepartament);
        await _context.SaveChangesAsync();
        return existingDepartament;
    }

    public async Task<Departament?> GetDepartamentByIdAsync(int id)
    {
        return await _context.Departaments
                             .Include(x => x.CreatedBy)
                             .Include(x => x.ModifiedBy)
                             .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Departament>> GetDepartamentsAsync()
    {
        return await _context.Departaments
                             .Include(x => x.CreatedBy)
                             .Include(x => x.ModifiedBy)
                             .ToListAsync();
    }
}
