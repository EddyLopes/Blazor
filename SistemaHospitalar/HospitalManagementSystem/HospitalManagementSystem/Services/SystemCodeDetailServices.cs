using HospitalManagementSystem.Data;
using HospitalManagementSystem.Interfaces;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services;

public class SystemCodeDetailServices : ISystemCodeDetailServices
{
    private readonly ApplicationDbContext _context;

    public SystemCodeDetailServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SystemCodeDetail> AddSystemCodeDetailAsync(SystemCodeDetail systemCodeDetail)
    {
        systemCodeDetail.CreatedOn = DateTime.UtcNow;
        await _context.SystemCodeDetails.AddAsync(systemCodeDetail);
        await _context.SaveChangesAsync();
        return systemCodeDetail;
    }

    public async Task<SystemCodeDetail?> UpdateSystemCodeDetailAsync(SystemCodeDetail systemCodeDetail)
    {
        var existingSystemDetailCode = await _context.SystemCodeDetails
                                               .FirstOrDefaultAsync(x => x.Id == systemCodeDetail.Id);

        if (existingSystemDetailCode is null)
            return null;

        existingSystemDetailCode.Code = systemCodeDetail.Code;
        existingSystemDetailCode.Description = systemCodeDetail.Description;
        existingSystemDetailCode.OrderNo = systemCodeDetail.OrderNo;
        existingSystemDetailCode.ModifiedOn = DateTime.UtcNow;
        existingSystemDetailCode.ModifiedById = systemCodeDetail.ModifiedById;

        _context.SystemCodeDetails.Update(existingSystemDetailCode);
        await _context.SaveChangesAsync();
        return existingSystemDetailCode;
    }

    public async Task<SystemCodeDetail?> DeleteSystemCodeDetailAsync(int id)
    {
        var existingSystemCodeDetail = await _context.SystemCodeDetails.FindAsync(id);
        if (existingSystemCodeDetail == null)
            return null;

        _context.SystemCodeDetails.Remove(existingSystemCodeDetail);
        await _context.SaveChangesAsync();
        return existingSystemCodeDetail;
    }

    public async Task<SystemCodeDetail?> GetSystemCodeDetailByIdAsync(int id)
    {
        return await _context.SystemCodeDetails
                             .Include(x => x.SystemCode)
                             .Include(x => x.CreatedBy)
                             .Include(x => x.ModifiedBy)
                             .FirstOrDefaultAsync(x => x.Id == id);
        
    }

    public async Task<List<SystemCodeDetail>> GetSystemCodeDetailsAsync()
    {
        return await _context.SystemCodeDetails
                     .Include(x => x.SystemCode)
                     .Include(x => x.CreatedBy)
                     .Include(x => x.ModifiedBy)
                     .ToListAsync();
    }
}
