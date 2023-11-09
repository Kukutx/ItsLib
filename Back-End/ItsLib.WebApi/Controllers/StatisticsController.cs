using ItsLib.DAL.Data;
using ItsLib.DAL.Repositories;
using ItsLib.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItsLib.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {

        private readonly Mapper _map;
        private ILibOfWork _repo;

        public StatisticsController(ILibOfWork repo, Mapper mapper)
        {
            _repo = repo;
            _map = mapper;
        }

        [HttpGet("AverageReviewsPerDay/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult AverageReviewsPerDay(Guid id)
        {
            Product? product = _repo.ProductRepo.Get(id);
            if (product == null)
                return NotFound("Prodotto non trovato");
            int days = StatisticsModel.Days(product.DateAdded);
            List<ProductUser>? users = _repo.ProductUserRepo.GetAll().FindAll(x => x.ProductId == id && x.IsDisabled == false);
            int i = 0;
            foreach (ProductUser user in users)
            {
                if (user.Review != null && user.ReviewTitle != null)
                    i++;
            }
            if (days <= 0 || i <= 0)
                return Ok(0);
            float avg = (float)i / days;
            StatisticsModel statistics = new StatisticsModel();
            statistics.Average = avg;
            return Ok(statistics);
        }

        [HttpGet("NumberOfAppearancesOnTheWishList/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult NumberOfAppearancesOnTheWishList(Guid id)
        {
            Product? product = _repo.ProductRepo.Get(id);
            if (product == null)
                return NotFound("Prodotto non trovato");
            List<ProductUser>? users = _repo.ProductUserRepo.GetAll().FindAll(x => x.ProductId == id && x.InWishList == true);
            if (users == null)
                return Ok(0);
            StatisticsModel statistics = new StatisticsModel();
            statistics.Average = users.Count();
            return Ok(statistics);
        }

        [HttpGet("AverageDailyWishlistEntries/{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult AverageDailyWishlistEntries(Guid id)
        {
            Product? product = _repo.ProductRepo.Get(id);
            if (product == null)
                return NotFound("Prodotto non trovato");
            int days = StatisticsModel.Days(product.DateAdded);
            List<ProductUser>? users = _repo.ProductUserRepo.GetAll().FindAll(x => x.ProductId == id && x.InWishList == true);
            if (days <= 0 || users.Count() <= 0)
                return Ok(0);
            float avg = users.Count() / (float)days;
            StatisticsModel statistics = new StatisticsModel();
            statistics.Average = avg;
            return Ok(statistics);
        }
    }
}
