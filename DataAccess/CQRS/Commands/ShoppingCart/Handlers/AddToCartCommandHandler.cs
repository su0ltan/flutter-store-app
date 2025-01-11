using DataAccess.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CQRS.Commands.ShoppingCart.Handlers
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand, bool>
    {
        private readonly AppDbContext _context;

        public AddToCartCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(i => i.UserId == request.UserId && i.ProductId == request.ProductId, cancellationToken);

            if (cartItem == null)
            {
                cartItem = new Data.Models.ShoppingCartItem
                {
                    UserId = request.UserId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += request.Quantity;
                _context.ShoppingCartItems.Update(cartItem);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
