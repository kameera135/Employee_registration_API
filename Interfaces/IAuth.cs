using EmployeeRegistration.DTO;

namespace EmployeeRegistration.Interfaces
{
    public interface IAuth
    {
        Task<bool> loginUser(AuthDTO user);
        Task<string> createToken(AuthDTO user);
    }
}
