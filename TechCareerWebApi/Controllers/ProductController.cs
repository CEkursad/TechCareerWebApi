using Microsoft.AspNetCore.Mvc;
using TechCareerWebApi.Models;

namespace TechCareerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {

        static List<Product> products;

        public ProductController()
        {
            if (products == null)
                products = ProductService.GetList();
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product1 = products.FirstOrDefault(x => x.Id == id);

            if (product1 == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product1);
            }
        }
        [HttpPost]
        public IActionResult Post(Product product)
        {
            product.Id = products.Count + 1;
            products.Add(product);

            return StatusCode(StatusCodes.Status201Created, product);
        }

        //delete by id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                products.Remove(product);
                return Ok(product);
            }
        }
    }
}
