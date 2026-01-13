using HospitalManagementSystem.Data;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;

public class SystemCodeServices : ISystemCodeServices
{
    private readonly ApplicationDbContext _context;

    public SystemCodeServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SystemCode> AddSystemCodeAsync(SystemCode systemCode)
    {
        systemCode.CreatedOn = DateTime.UtcNow;
        await _context.SystemCodes.AddAsync(systemCode);
        await _context.SaveChangesAsync();
        return systemCode;
    }

    public async Task<SystemCode?> UpdateSystemCodeAsync(SystemCode systemCode)
    {
        var existingSystemCode = await _context.SystemCodes
                                     .FirstOrDefaultAsync(x => x.Id == systemCode.Id);

        if (existingSystemCode is null)
            return null;

        existingSystemCode.Code = systemCode.Code;
        existingSystemCode.Description = systemCode.Description;
        existingSystemCode.OrderNo = systemCode.OrderNo;
        existingSystemCode.ModifiedOn = DateTime.UtcNow;
        existingSystemCode.ModifiedById = systemCode.ModifiedById;
        
        _context.SystemCodes.Update(existingSystemCode);
        await _context.SaveChangesAsync();
        return existingSystemCode;
    }

    public async Task<SystemCode?> DeleteSystemCodeAsync(int id)
    {
        var existingSystemCode = await _context.SystemCodes.FindAsync(id);
        if (existingSystemCode == null)
            return null;

        _context.SystemCodes.Remove(existingSystemCode);
        await _context.SaveChangesAsync();
        return existingSystemCode;
    }

    public async Task<SystemCode?> GetSystemCodeByIdAsync(int id)
    {
        return await _context.SystemCodes
                             .Include(x => x.CreatedBy)
                             .Include(x => x.ModifiedBy)
                             .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<SystemCode>> GetSystemCodesAsync()
    {
        var systemCodes = await _context.SystemCodes
                                        .Include(x => x.CreatedBy)
                                        .Include(x => x.ModifiedBy)
                                        .ToListAsync();
        return systemCodes;
    }  
}
