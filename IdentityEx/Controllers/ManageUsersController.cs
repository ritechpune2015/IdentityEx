using IdentityEx.Dtos;
using IdentityEx.Models;
using IdentityEx.Services;
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
        SignInManager<User> _signinmanager; 
        IToken _tokensvr;
        public ManageUsersController(UserManager<User> usermanager,IToken tokensvr,SignInManager<User> signin)
            {
            this._usermanager = usermanager;
            this._tokensvr = tokensvr;
            this._signinmanager = signin;
            }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello!");
        }

        

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto rec)
        {
            LoginResultDto logres = new LoginResultDto();

            //find user if exits 
            var urec = this._usermanager.Users.FirstOrDefault(p => p.UserName == rec.EmailID);
            if (urec == null)
            {
                logres.IsSuccess = false;
                logres.Message = "User Name do not exits!";
                return BadRequest(logres);
            }

            var status=await this._signinmanager.CheckPasswordSignInAsync(urec, rec.Password,false);
            if (!status.Succeeded)
            {
                logres.IsSuccess = false;
                logres.Message = "Invalid Email Id or Password!";
                return BadRequest(logres);
            }
            else
            {
                logres.IsSuccess = true;
                logres.Name = urec.FullName;
                logres.Token = this._tokensvr.GenerateToken(urec);
                return Ok(logres);
            }
            
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
