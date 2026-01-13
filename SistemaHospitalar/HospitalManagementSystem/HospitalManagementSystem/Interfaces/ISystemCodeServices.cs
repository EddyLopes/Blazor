using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Interfaces;

public interface ISystemCodeServices
{
    Task<List<SystemCode>> GetSystemCodesAsync();
    Task<SystemCode?> GetSystemCodeByIdAsync(int id);    
    Task<SystemCode> AddSystemCodeAsync(SystemCode systemCode);
    Task<SystemCode?> UpdateSystemCodeAsync(SystemCode systemCode);
    Task<SystemCode?> DeleteSystemCodeAsync(int id);
}
