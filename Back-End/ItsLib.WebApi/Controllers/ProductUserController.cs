using ItsLib.DAL.Data;
using ItsLib.DAL.Repositories;
using ItsLib.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Security.Claims;

namespace ItsLib.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUserController : ODataController
    {
        private readonly Mapper _map;
        private ILibOfWork _repo;
        private readonly UserManager<User> _userManager;

        public ProductUserController(ILibOfWork repo, Mapper map, UserManager<User> userManager)
        {
            _repo = repo;
            _map = map;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(ODataQueryOptions<ProductUser> options)
        {
            return Ok(_repo.ProductUserRepo.GetAll(options));
        }

        [HttpPost("whislist")]
        [Authorize]
        public async Task<IActionResult> CreateWhis([FromBody] PostInWhisList postInWhisList)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return BadRequest("Utente non trovato");
            if (_repo.ProductRepo.Get(postInWhisList.ProductId) != null && _repo.UserRepo.Get(user.Id) != null)
            {
                ProductUser? p = _repo.ProductUserRepo.GetUserProduct(user.Id, postInWhisList.ProductId);
                if (p != null)
                {
                    p.InWishList = !p.InWishList;
                    p.LastModifiedDate = DateTime.Now;
                    if (_repo.ProductUserRepo.Update(p))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                ProductUser productUser = _repo.ProductUserRepo.Create(_map.MapPostInWhisList(postInWhisList, user.Id));
                return Ok();
            }
            return BadRequest("Non sono riuscito a creare il prodotto");
        }

        [HttpPost("used")]
        [Authorize]
        public async Task<IActionResult> CreateUsed([FromBody] PostIsUsed postIsUsed)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return BadRequest("Utente non trovato");
            if (_repo.ProductRepo.Get(postIsUsed.ProductId) != null && _repo.UserRepo.Get(user.Id) != null)
            {
                ProductUser? p = _repo.ProductUserRepo.GetUserProduct(user.Id, postIsUsed.ProductId);
                if (p != null)
                {
                    p.IsUsed = !p.IsUsed;
                    p.LastModifiedDate = DateTime.Now;
                    if (_repo.ProductUserRepo.Update(p))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                ProductUser productUser = _repo.ProductUserRepo.Create(_map.MapPostIsUsed(postIsUsed, user.Id));
                return Ok();
            }
            return BadRequest("Non sono riuscito a creare il prodotto");
        }

        [HttpPut("createreview")]
        [Authorize]
        public async Task<IActionResult> CreateReview([FromBody] PostReview postReview)
        {

            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return BadRequest("Utente non trovato");
            Product prod = _repo.ProductRepo.Get(postReview.ProductId);
            if (prod == null)
                return NotFound("Prodotto non trovato");
            ProductUser? productUser = _repo.ProductUserRepo.GetUserProduct(user.Id, postReview.ProductId);
            if (productUser == null)
                return NotFound("Prodotto non trovato");
            if (productUser.IsUsed == false)
                return BadRequest("Il ptodotto non è stato utilizzato");
            if (productUser.Review == null && productUser.ReviewTitle == null)
            {
                if (_repo.ProductUserRepo.ReviewNumber(user.Id) % 3 == 0)
                {
                    DiscountCode discountCode = _repo.DiscountCodeRepo.Create(_map.MapPostDiscountCode
                        (_repo.DiscountCodeRepo.NewDiscount(), _repo.DiscountCodeRepo.NewDiscountCode()));
                    PostUserDiscountCodeModel postUserDiscountCodeModel = new PostUserDiscountCodeModel();
                    postUserDiscountCodeModel.DiscountCodeId = discountCode.DiscountCodeId;
                    postUserDiscountCodeModel.UserId = user.Id;
                    UserDiscountCode userDiscountCode = _repo.UserDiscountCodeRepo.Create(_map.MapPostUserDiscountCode(postUserDiscountCodeModel));
                    productUser.ReviewTitle = postReview.ReviewTitle;
                    productUser.Review = postReview.Review;
                    productUser.LastModifiedDate = DateTime.Now;
                    if (_repo.ProductUserRepo.Update(productUser))
                        return Ok();
                    return BadRequest("Operazione non completata");

                }
            }
            productUser.ReviewTitle = postReview.ReviewTitle;
            productUser.Review = postReview.Review;
            productUser.LastModifiedDate = DateTime.Now;
            if (_repo.ProductUserRepo.Update(productUser))
                return Ok();
            return BadRequest("Operazione non completata");
        }

        [HttpPut("DeleteReview/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            User? user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user == null)
                return BadRequest("Utente non trovato");
            ProductUser productUser = _repo.ProductUserRepo.GetUserProduct(user.Id, id);
            if (productUser == null)
                return NotFound("Prodotto non trovato");
            productUser.ReviewTitle = null;
            productUser.Review = null;
            productUser.LastModifiedDate = DateTime.Now;
            if (_repo.ProductUserRepo.Update(productUser))
                return Ok();
            return BadRequest("Operazione non completata");
        }

    }
}
