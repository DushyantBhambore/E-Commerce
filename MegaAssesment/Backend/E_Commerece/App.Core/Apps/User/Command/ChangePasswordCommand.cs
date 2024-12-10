using App.Core.Dtos;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Command
{
    public class ChangePasswordCommand : IRequest<UserResponseModel>
    {
        public ChangePasswordDto ChangePasswordDto { get; set; }
    }
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, UserResponseModel>
    {
        private readonly IAppDbContext _context;

        public ChangePasswordCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<UserResponseModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var model = request.ChangePasswordDto;
            var user = await _context.Set<Domain.User>()
                .FirstOrDefaultAsync(x => x.Username == request.ChangePasswordDto.UserName);
            if (user == null)
            {
                return new UserResponseModel((int)HttpStatusCode.BadRequest, "Username is Invalid", request.ChangePasswordDto.UserName);
            }

           

            if (request.ChangePasswordDto.ConfirmPassword != request.ChangePasswordDto.NewPassword)
            {
                return new UserResponseModel((int)HttpStatusCode.BadRequest, "Password is not Matched ", null);
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);

            user.Password = hashedPassword;
            await _context.SaveChangesAsync();

            //_context.Set<Domain.User>().Update(user);
            return new UserResponseModel((int)HttpStatusCode.OK, "Password Changes SuccessFully",null);
        }

    }
}
