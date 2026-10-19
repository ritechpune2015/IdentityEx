using IdentityEx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        CompanyContext cntx;
        public ProductAPIController(CompanyContext cntx)
        {
            this.cntx = cntx;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.cntx.Products.ToList());
        }
    }
}
