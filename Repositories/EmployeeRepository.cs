using EmployeeRegistration.DTO;
using EmployeeRegistration.Interfaces;
using EmployeeRegistration.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegistration.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly EmployeeContext m_employeeContext;
        private readonly IDbContextFactory<EmployeeContext> m_dbContext;


        public EmployeeRepository(EmployeeContext context, IDbContextFactory<EmployeeContext> dbContext)
        {
            m_employeeContext = context;
            m_dbContext = dbContext;
        }

        public Task<List<EmployeeDTO>> getEmployee()
        {
            try
            {
                List<Employee> employees = new List<Employee>();

                employees = m_employeeContext.Employees.Where(q=>q.DeletedAt == null).ToList();

                if(employees == null || employees.Count == 0)
                {
                    throw new Exception("There is no employees");
                }

                List<EmployeeDTO> result = new List<EmployeeDTO>();

                foreach(Employee employee in employees)
                {
                    EmployeeDTO tempValue = new()
                    {
                        id = employee.Id,
                        name = employee.Name,
                        epf = employee.Epf,
                        address = employee.Address,
                        mobile = employee.Mobile,
                        email = employee.Email,
                    };

                    result.Add(tempValue);
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> postEmployee(PostEmployeeDTO employee)
        {
            try
            {
                using var _dbContext = m_dbContext.CreateDbContext();

                Employee tempEmployee = new Employee
                {
                    Name = employee.name,
                    Epf = employee.epf,
                    Address = employee.address,
                    Mobile = employee.mobile,
                    Email = employee.email
                };

                _dbContext.Add(tempEmployee);
                return await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
