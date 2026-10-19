using IdentityEx.Dtos;
using IdentityEx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUsersController : ControllerBase
    {
        UserManager<User> _usermanager;
        public ManageUsersController(UserManager<User> usermanager)
            {
            this._usermanager = usermanager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello!");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto rec)
        {
            var urec = new User() {
                 FirstName=rec.FirstName,
                  LastName=rec.LastName,
                  Address=rec.Address,
                  Email=rec.EmailID,
                  MobileNo=rec.MobileNo,    
                  UserName =rec.EmailID
            };
            var res=await this._usermanager.CreateAsync(urec, rec.Password);
            if (res.Succeeded)
            {
                return Ok("User Registration Successfull!!!");
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var err in res.Errors)
                {
                    errors.Add(err.Description);
                }

                return BadRequest(errors);
            }
        }


       

    }
}
