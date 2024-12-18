using EmployeeRegistration.DTO;

namespace EmployeeRegistration.Interfaces
{
    public interface IEmployee
    {
        Task<List<EmployeeDTO>> getEmployee();

        Task<int> postEmployee(PostEmployeeDTO employee);

        //Task<int> updateEmployee(DTO product, long updatedBy);
    }
}
