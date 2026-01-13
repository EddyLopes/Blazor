using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Interfaces;

public interface IDepartamentServices
{
    Task<Departament> AddDepartamentAsync(Departament departament);
    Task<Departament?> UpdateDepartamentAsync(Departament departament);
    Task<Departament?> DeleteDepartamentAsync(int id);
    Task<Departament?> GetDepartamentByIdAsync(int id);
    Task<List<Departament>> GetDepartamentsAsync();
}
