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


       
        //[Route("GetAll")]
        //public IActionResult GetAll()
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Rol = new ML.Rol();

        //    ML.Result result = _usuario.GetAllLINQ();

        //    if (result.Correct)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, result.ErrorMessage);
        //    }
        //}

        // GET api/<Product>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Product>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<Product>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<Product>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
