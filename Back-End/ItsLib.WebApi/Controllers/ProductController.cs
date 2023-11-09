using ItsLib.DAL.Data;
using ItsLib.DAL.Repositories;
using ItsLib.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ItsLib.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ODataController
    {
        private readonly Mapper _map;
        private ILibOfWork _repo;

        public ProductController(ILibOfWork repo, Mapper mapper)
        {
            _repo = repo;
            _map = mapper;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Get(ODataQueryOptions<Product> options)
        {
            return Ok(_repo.ProductRepo.GetAll(options));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create([FromBody] PostProductModel postProductModel)
        {
            if (_repo.CategoryRepo.Get(postProductModel.CategoryId) != null)
            {
                Product product = _repo.ProductRepo.Create(_map.MapPostProduct(postProductModel));
                return Ok();
            }
            return BadRequest("Non sono riuscito a creare il prodotto");
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult PutProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Prodotto non trovato");
            if (_repo.ProductRepo.Update(product))
                return Ok();
            return BadRequest();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Patch(Guid id, [FromBody] JsonPatchDocument<Product> patch)
        {
            var ret = _repo.ProductRepo.Patch(patch, id);

            if (ret)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }


        //[HttpPut("disable/{id}")]
        //public IActionResult Switch(Guid id)
        //{
        //    Product? product = _repo.ProductRepo.Get(id);
        //    if (product == null)
        //        return BadRequest("Prodotto non trovato");
        //    if (product.IsDisabled == true)
        //        return BadRequest("Il prodotto è già disabilitato");
        //    if (_repo.ProductRepo.Update(product))
        //        return Ok();
        //    return BadRequest();
        //}


        //[HttpDelete]
        //[Authorize(Roles = UserRoles.Admin)]
        //public IActionResult Delete(Guid id)
        //{
        //    Product? product = _repo.ProductRepo.Get(id);
        //    if (product == null)
        //        return BadRequest();
        //    if (_repo.ProductRepo.Delete(id))
        //        return Ok();
        //    return BadRequest();
        //}

    }
}
