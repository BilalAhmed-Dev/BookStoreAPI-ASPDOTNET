using Microsoft.AspNetCore.Mvc;
using BookStoreApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookStoreApp.API.Models;
using BookStoreApp.API.Service;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(ICartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Msg>> AddCartItem([FromQuery] int bookId)
        {
            var userId = GetUserId();
            return await _cartService.AddCartItem(bookId, userId);
        }

        [HttpGet("getCart")]
        public async Task<ActionResult<List<CartItem>>> GetCart()
        {
            var userId = GetUserId();
            return await _cartService.GetCartByUserId(userId);
        }

        [HttpPut("decreaseCartAmount")]
        public async Task<ActionResult<Msg>> DecreaseCartAmount([FromQuery] int bookId)
        {
            var userId = GetUserId();
            return await _cartService.DecreaseAmount(bookId, userId);
        }

        [HttpDelete("deleteCartItem")]
        public async Task<ActionResult<Msg>> DeleteCartItem([FromQuery] int bookId)
        {
            var userId = GetUserId();
            return await _cartService.DeleteCartItem(bookId, userId);
        }

        [HttpDelete("deleteAllCartItem")]
        public async Task<ActionResult<Msg>> DeleteAllCartItem()
        {
            var userId = GetUserId();
            return await _cartService.DeleteAll(userId);
        }
    }
}
