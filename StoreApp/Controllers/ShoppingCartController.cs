using DataAccess.CQRS.Commands.ShoppingCart;
using DataAccess.CQRS.Queries.ShoppingCart;
using DataAccess.Data.DTOs.ShoppingCartDto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace StoreApp.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            var result = await _mediator.Send(new AddToCartCommand
            {
                /* UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),*/ // Assuming JWT authentication
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            });

            if (!result) return BadRequest();

            return Ok();
        }

        [HttpDelete("")]
        public async Task<IActionResult> RemoveFromCart(int productId, string userID)
        {
            var result = await _mediator.Send(new RemoveFromCartCommand
            {

                UserId = userID,
                
                ProductId = productId
            });

            if (!result) return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _mediator.Send(new GetCartQuery
            {
                /*UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)*/
                UserId = userId
            });

            return Ok(cart);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateCartItem(int productId, UpdateCartItemDto dto)
        {
            var result = await _mediator.Send(new UpdateCartItemCommand
            {
              /*  UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),*/
              UserId = dto.userId,
                ProductId = productId,
                Quantity = dto.Quantity
            });

            if (!result) return NotFound();

            return NoContent();
        }
    }

}
