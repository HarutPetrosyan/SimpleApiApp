using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleApi.Models.Models;
using SimpleApi.Repositories.Login;
using SimpleApi.Repositories.Operation;
using SimpleApi.Repositories.Registration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SimpleApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class SimpleController : ControllerBase
    {
        private static RegistrationModel user = new RegistrationModel();
        private static LoginModel login = new LoginModel();
        private static HashingModel PassHash = new HashingModel();
        private readonly IRegistrationRepository registration;
        private readonly ILoginRepository loginRepository;
        private readonly IOperationRepository repository;
        private IConfiguration Configuration;
        public SimpleController(IConfiguration configuration, IRegistrationRepository Registration, ILoginRepository _loginRepository,IOperationRepository _repository)
        {
            Configuration = configuration;
            registration = Registration;
            loginRepository = _loginRepository;
            repository = _repository;
        }

        [HttpPost]
        [Route("Registrtion")]
        public async Task<ActionResult<RegistrationModel>> Registrtion(RegistrationModel UserRegistr)
        {
            user = UserRegistr;
            login.Login = user.Login;
            login.Password = user.Password;
            CreatePasswordHash(login.Password, out byte[] passwordHash, out byte[] passwordSalt);
            PassHash.PasswordHash = passwordHash;
            PassHash.PasswordSalt = passwordSalt;
            registration.AddUser(user);
            return Ok(user);
        }
        [HttpPost]
        [Route("App/Login")]
        public async Task<ActionResult<string>> LoginUser(LoginModel model)
        {
            LoginModel loginModel = loginRepository.Get(model.Login, model.Password);
            user.Login = loginModel.Login;
            user.Password = loginModel.Password;
            if (loginModel.Login == null)
                BadRequest("User Not Found");


            string token = CreateToken(user);
            return Ok(token);
        }

        [HttpGet,Authorize]
        [Route("Get/UserInfo")]
        public  RegistrationModel GetUserInfo()
        {
          var result=  repository.Get(user.Login, user.Password);
            return result;
        }

        [HttpPut,Authorize]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(RegistrationModel user)
        {
            registration.UpdateUser(user);
            return Ok(user);
        }
        [HttpDelete,Authorize]
        [Route("Delete")]
        public IActionResult DeleteUser(int Id)
        {
            registration.DeleteUser(Id);
            return Ok();
        }


        #region MyToken
        private string CreateToken(RegistrationModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role,"Admin")
            };
            var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                Configuration.GetSection("AppSetings:Token").Value));
            var cred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
            var Token = new JwtSecurityToken(
                 claims: claims,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials: cred
                );
            var Jwt = new JwtSecurityTokenHandler().WriteToken(Token);

            return Jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }


        }
        #endregion
    }
}
