using DataAccess.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CQRS.Commands.Products.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly AppDbContext _context;

        public UpdateProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null) return false;

            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.LastUpdate = DateTime.UtcNow;

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
