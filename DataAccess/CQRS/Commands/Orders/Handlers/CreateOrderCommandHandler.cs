using DataAccess.Data.Models;
using DataAccess.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.CQRS.Commands.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly AppDbContext _context;

        public CreateOrderCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Create the order
            var order = new Order
            {
                UserId = request.UserId,
                Amount = 0, // Will be calculated later
                DateCreated = DateTime.UtcNow,
                ConfirmationNumber = request.ConfirmationNumber
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            // Calculate total amount and add items
            decimal totalAmount = 0;
            foreach (var item in request.Items)
            {
                // Fetch product details from the database
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId, cancellationToken);

                if (product == null)
                    throw new Exception($"Product with ID {item.ProductId} not found.");

                totalAmount += product.Price * item.Quantity;

                var orderProduct = new OrderHasProduct
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                _context.OrderProducts.Add(orderProduct);
            }

            // Update the order's total amount
            order.Amount = totalAmount;
            await _context.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }


}
