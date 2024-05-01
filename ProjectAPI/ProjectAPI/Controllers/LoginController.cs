using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using ProjectAPI.Models;

namespace WebApplication5.Controllers
{
    [Route("api/Login")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly TokenService tokenServices;
        private readonly BankContext context;
        private readonly BankContext _bankContext;

        public LoginController(TokenService configuration, BankContext bankcontext)
        {
            tokenServices = configuration;
            this.context = bankcontext;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegistration userRegistration)
        {
            bool isAdmin = false;
            if (context.UserAccounts.Count() == 0)
            {
                isAdmin = true;
            }

            var newAccount = UserAccounts.Create(userRegistration.Username, userRegistration.Password, isAdmin);

            context.UserAccounts.Add(newAccount);
            context.SaveChanges();

            return Ok(new { Message = "User Created" });
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginReguest user)
        {
            var response = this.tokenServices.GenerateToken(user.Username, user.Password);

            if (response.IsValid)
            {
                return Ok(new { Token = response.Token });
            }
            else
            {
                return BadRequest("Username and/or Password is wrong");
            }
        }
    }
}
