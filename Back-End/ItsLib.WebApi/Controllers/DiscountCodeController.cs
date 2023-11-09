using ItsLib.DAL.Data;
using ItsLib.DAL.Repositories;
using ItsLib.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ItsLib.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountCodeController : ODataController
    {
        private readonly Mapper _map;
        private ILibOfWork _repo;

        public DiscountCodeController(ILibOfWork repo, Mapper mapper)
        {
            _repo = repo;
            _map = mapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(ODataQueryOptions<UserDiscountCode> options)
        {
            return Ok(_repo.UserDiscountCodeRepo.GetAll(options));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create()
        {
            DiscountCode discountCode = _repo.DiscountCodeRepo.
                Create(_map.MapPostDiscountCode
                (_repo.DiscountCodeRepo.NewDiscount(), _repo.DiscountCodeRepo.NewDiscountCode()));
            return Ok(_map.MapEntityToModel(discountCode));
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Switch(Guid id)
        {
            DiscountCode? discountCode = _repo.DiscountCodeRepo.Get(id);
            if (discountCode == null)
                return BadRequest();
            discountCode.IsDisabled = !discountCode.IsDisabled;
            if (_repo.DiscountCodeRepo.Update(discountCode))
                return Ok();
            return BadRequest();
        }

        [HttpDelete]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult Delete(Guid id)
        {
            DiscountCode? discountCode = _repo.DiscountCodeRepo.Get(id);
            if (discountCode == null)
                return BadRequest();
            if (_repo.DiscountCodeRepo.Delete(id))
                return Ok();
            return BadRequest();
        }

    }
}
