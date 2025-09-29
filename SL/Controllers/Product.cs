using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Product : ControllerBase
    {
        // GET: api/<Product>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ML.Products product = new ML.Products();
            product.Supplier = new ML.Suppliers();
            product.Category = new ML.Categories();

            ML.Result result = BL.Product.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult Add(ML.Products products)
        {
            ML.Products product = new ML.Products();
            product.Supplier = new ML.Suppliers();
            product.Category = new ML.Categories();

            ML.Result result = BL.Product.Add(products);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(ML.Products products)
        {
            ML.Products product = new ML.Products();
            product.Supplier = new ML.Suppliers();
            product.Category = new ML.Categories();

            ML.Result result = BL.Product.Update(products);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
        }

        // GET api/<Product>/5
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {

            ML.Result result = BL.Product.GetById(id);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
        }

        // DELETE api/<Product>/5
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            ML.Result result = BL.Product.Delete(id);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
            }
        }
    }
}
