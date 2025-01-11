using DataAccess.Data.DTOs.ShoppingCartDto;
using DataAccess.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.CQRS.Queries.ShoppingCart.Handlers
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, ShoppingCartDto>
    {
        private readonly AppDbContext _context;

        public GetCartQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cartItems = await _context.ShoppingCartItems
                .Where(i => i.UserId == request.UserId)
                .Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = i.Product.Price,
                    Quantity = i.Quantity
                })
                .ToListAsync(cancellationToken);

            var totalAmount = cartItems.Sum(i => i.TotalPrice);

            return new ShoppingCartDto
            {
                Items = cartItems,
                TotalAmount = totalAmount
            };
        }
    }
}
