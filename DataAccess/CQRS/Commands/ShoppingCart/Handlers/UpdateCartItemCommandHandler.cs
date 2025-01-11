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
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateCartItemCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(i => i.UserId == request.UserId && i.ProductId == request.ProductId, cancellationToken);

            if (cartItem == null)
                return false;

            cartItem.Quantity = request.Quantity;
            _context.ShoppingCartItems.Update(cartItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
