using EmployeeRegistration.DTO;
using EmployeeRegistration.Interfaces;
using EmployeeRegistration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeRegistration.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EmployeeContext m_context;
        private readonly IAuth m_auth;

        public AuthController(EmployeeContext _context, IAuth _auth)
        {
            m_context = _context;
            m_auth = _auth;
        }

        [HttpPost("user/login")]
        public async Task<IActionResult> LoginUser(AuthDTO user)
        {
            try
            {
                var username = m_context.Users.Where(u=>u.UserName == user.userName).FirstOrDefault();

                if(username == null)
                {
                    var errorResponse = new
                    {
                        Status = "Fail",
                        Message = "Wrong Username",
                    };

                    return StatusCode(StatusCodes.Status400BadRequest, errorResponse);
                }

                var result = await m_auth.loginUser(user);
                var handler = new JwtSecurityTokenHandler();

                if(!result)
                {
                    var errorResponse = new
                    {
                        Status = "Fail",
                        Message = "Wrong Password",
                    };

                    return StatusCode(StatusCodes.Status400BadRequest, errorResponse);
                }

                string token = await m_auth.createToken(user);

                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    message = "Login Successfully",
                    Token = token,
                    Expire = handler.ReadToken(token).ValidTo,
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
