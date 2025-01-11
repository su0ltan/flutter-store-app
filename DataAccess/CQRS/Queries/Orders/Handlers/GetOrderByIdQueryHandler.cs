using DataAccess.Data.DTOs.Orders;
using DataAccess.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.CQRS.Queries.Orders.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly AppDbContext _context;

        public GetOrderByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Where(o => o.Id == request.OrderId)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    TotalAmount = o.Amount,
                    DateCreated = o.DateCreated,
                    ConfirmationNumber = o.ConfirmationNumber,
                    Items = o.OrderProducts.Select(op => new OrderItemDto
                    {
                        ProductId = op.ProductId,
                        ProductName = op.Product.Name,
                        Price = op.Product.Price,
                        Quantity = op.Quantity
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            return order;
        }
    }

}
