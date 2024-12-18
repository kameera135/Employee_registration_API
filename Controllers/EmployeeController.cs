using EmployeeRegistration.DTO;
using EmployeeRegistration.Interfaces;
using EmployeeRegistration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRegistration.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee m_employee;
        private readonly EmployeeContext m_employeeContext;

        public EmployeeController(IEmployee employee, EmployeeContext context)
        {
            m_employeeContext = context;
            m_employee = employee;
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                List<EmployeeDTO> result = new List<EmployeeDTO>();

                result = await m_employee.getEmployee();

                return Ok(result);
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Message = "Internal Server Error",
                    ErrorDetails = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
            finally
            {

            }
        }

        [HttpPost("employee")]
        public async Task<IActionResult> PostEmployee(PostEmployeeDTO employee)
        {
            try
            {
                int result = await m_employee.postEmployee(employee);

                if (result == 0)
                {
                    var errorResponse = new
                    {
                        Message = "Internal Server Error",
                        ErrorDetails = "Databese is not updated"
                    };

                    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                }

                return Ok(new
                {
                    status_code = 200,
                    status = "Success",
                    message = "User added successfully"
                });
            }
            catch (Exception ex)
            {
                var errorResponse = new
                {
                    Message = "Internal Server Error",
                    ErrorDetails = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
            finally
            {

            }
        }
    }
}
