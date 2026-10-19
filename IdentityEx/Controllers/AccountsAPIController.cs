using IdentityEx.Models;
using IdentityEx.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsAPIController : ControllerBase
    {
        IToken token;
        public AccountsAPIController(IToken token)
        {
            this.token = token;
        }

        [HttpPost]
        public IActionResult Login()
        {
            User rec = new User()
            {
                FirstName = "Sunil",
                LastName = "Jadhav",
                Email = "sunil@gmail.com",
                Address = "Akurdi"

            };
            return Ok(this.token.GenerateToken(rec));
        }
    }
}
