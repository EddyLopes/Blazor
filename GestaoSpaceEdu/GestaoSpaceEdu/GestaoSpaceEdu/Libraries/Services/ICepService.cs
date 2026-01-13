namespace GestaoSpaceEdu.Libraries.Services
{
    public interface ICepService
    {
        Task<LocalAddress?> SearchByPostalCode(string postalCode);
    }
}