using AutoMapper;
using IdentityEx.Dtos;
using IdentityEx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IdentityEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductAPIController : ControllerBase
    {
        CompanyContext cntx;
        IMapper map;
        public ProductAPIController(CompanyContext cntx,IMapper  map)
        {
            this.cntx = cntx;
            this.map = map;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            //var res = from t in this.cntx.Products
            //          select new ProductDto
            //          {
            //              ProductID= t.ProductID,
            //              ProductName=t.ProductName,
            //              CategoryName=t.Category.CategoryName,
            //              MfgName=t.MfgName,
            //              Price=t.Price
            //          };

            var res = this.map.Map<List<ProductDto>>(this.cntx.Products.ToList());

            return Ok(res.ToList());
        }
    }

}
