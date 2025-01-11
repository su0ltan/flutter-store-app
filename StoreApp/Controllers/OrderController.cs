using DataAccess.CQRS.Commands.Orders;
using DataAccess.CQRS.Queries.Orders;
using DataAccess.Data.DTOs.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var orderId = await _mediator.Send(new CreateOrderCommand
            {
                UserId = dto.UserId,
                ConfirmationNumber = dto.ConfirmationNumber,
                Items = dto.Items // Pass only ProductId and Quantity
            });

            return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, orderId);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery { OrderId = id });
            if (order == null) return NotFound();
            return Ok(order);
        }

    }

}
