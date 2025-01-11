using DataAccess.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CQRS.Queries.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly UserManager<AppUser> _userManager;

        public UpdateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return false;

            user.UserName = request.Name; // maybe need to change from AppUser model, and userDTOs
            user.PhoneNumber = request.Phone;
            user.Address = request.Address;
            user.CityRegion = request.CityRegion;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
