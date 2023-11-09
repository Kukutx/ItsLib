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
    public class CategoryController : ODataController
    {
        private readonly Mapper _map;
        private ILibOfWork _repo;

        public CategoryController(ILibOfWork repo, Mapper mapper)
        {
            _repo = repo;
            _map = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(ODataQueryOptions<Category> options)
        {
            return Ok(_repo.CategoryRepo.GetAll(options));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Create([FromBody] PostCategoryModel postCategoryModel)
        {
            Category category = _repo.CategoryRepo.Create(_map.MapPostCategory(postCategoryModel));
            return Ok(_map.MapEntityToModel(category));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Patch(Guid id, [FromBody] JsonPatchDocument<Category> patch)
        {
            var ret = _repo.CategoryRepo.Patch(patch, id);

            if (ret)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        //[HttpDelete]
        //[Authorize(Roles = UserRoles.Admin)]
        //public IActionResult Delete(Guid id)
        //{
        //    Category? category = _repo.CategoryRepo.Get(id);
        //    if (category == null)
        //        return BadRequest();
        //    if (_repo.CategoryRepo.Delete(id))
        //        return Ok();
        //    return BadRequest();
        //}

        //[HttpPut("{id}")]
        //[Authorize(Roles = UserRoles.Admin)]
        //public IActionResult Switch(Guid id)
        //{
        //    Category? category = _repo.CategoryRepo.Get(id);
        //    if (category == null)
        //        return BadRequest();
        //    category.IsDisabled = !category.IsDisabled;
        //    if (_repo.CategoryRepo.Update(category))
        //        return Ok();
        //    return BadRequest();
        //}

    }
}
