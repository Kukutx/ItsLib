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
    public class UserDiscountCodeController : ODataController
    {
        private readonly Mapper _map;
        private ILibOfWork _repo;

        public UserDiscountCodeController(ILibOfWork repo, Mapper mapper)
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
        public IActionResult Create([FromBody] PostUserDiscountCodeModel postUserDiscountCodeModel)
        {
            if (_repo.DiscountCodeRepo.Get(postUserDiscountCodeModel.DiscountCodeId) != null
                && _repo.UserRepo.Get(postUserDiscountCodeModel.UserId) != null)
            {
                UserDiscountCode userDiscountCode = _repo.UserDiscountCodeRepo.Create(_map.MapPostUserDiscountCode(postUserDiscountCodeModel));
                return Ok(_map.MapEntityToModel(userDiscountCode));
            }
            return BadRequest("Non sono riuscito a creare il prodotto");
        }
    }
}
