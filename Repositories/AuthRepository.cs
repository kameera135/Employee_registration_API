using EmployeeRegistration.DTO;
using EmployeeRegistration.Interfaces;
using EmployeeRegistration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeRegistration.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly EmployeeContext m_context;
        private readonly IConfiguration m_configuration;

        public AuthRepository(EmployeeContext context, IConfiguration configuration)
        {
            m_context = context;
            m_configuration = configuration;

        }

        public async Task<string> createToken(AuthDTO user)
        {
            try
            {
                List<Claim> claims = new List<Claim>();

                User? userDetails = m_context.Users.Where(u=>u.UserName == user.userName).FirstOrDefault();

                if (userDetails == null)
                {
                    throw new Exception("User not found");
                }

                claims.Add(new Claim("id", userDetails.Id.ToString()));
                claims.Add(new Claim("username", userDetails.UserName.ToString()));
                claims.Add(new Claim("name", userDetails.Name.ToString()));


                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: m_configuration.GetSection("Jwt:Key").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(7), //make this more limit time like minitue
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> loginUser(AuthDTO user)
        {
            try
            {
                User? tempUser = m_context.Users.Where(u => u.UserName == user.userName && u.DeletedAt == null).FirstOrDefault();

                if (tempUser == null && tempUser.UserName == null)
                {
                    return false;
                }

                string incomingPasswordHash = getPasswordHash(user.password, tempUser.PasswordSalt);

                if (incomingPasswordHash != tempUser.PasswordHash)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getPasswordHash(string password, string salt)
        {
            // Combine the password and salt
            string combinedPassword = MD5(salt) + password;

            // Choose the hash algorithm (SHA-256 or SHA-512)
            using (var sha512 = SHA512.Create())
            {
                // Convert the combined password string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(combinedPassword);

                // Compute the hash value of the byte array
                byte[] hash = sha512.ComputeHash(bytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("x2"));
                }

                return result.ToString();
            }
        }

        protected string MD5(string text)
        {
            using var provider = System.Security.Cryptography.MD5.Create();
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(text)))
                builder.Append(b.ToString("x2").ToLower());

            return builder.ToString();
        }
    }
}
