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
    public class UserController : ODataController
    {
        private ILibOfWork _repo;
        private readonly Mapper _map;


        public UserController(ILibOfWork repo, Mapper map)
        {
            _repo = repo;
            _map = map;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(ODataQueryOptions<User> options)
        {
            return Ok(_repo.UserRepo.GetAll(options));
        }

    }
}
