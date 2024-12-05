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

namespace App.Core.Apps.PayementCardTable.Command
{
    public class AddOrUpdateAddressCommand : IRequest<PaymentResponseModel>
    { 
        public int UserId { get; set; } 
        public string Address { get; set; } = string.Empty;
    }
    public class AddOrUpdateAddressCommandHandller : IRequestHandler<AddOrUpdateAddressCommand, PaymentResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public AddOrUpdateAddressCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PaymentResponseModel> Handle(AddOrUpdateAddressCommand request, CancellationToken cancellationToken)
        {

            var user = await _appDbContext.Set<Domain.User>().FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

            if (user == null)
            {

                return new PaymentResponseModel((int)HttpStatusCode.BadRequest, "User not found.", null);
            }

            user.Address = request.Address;
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return new PaymentResponseModel((int)HttpStatusCode.OK, "Address updated successfully.", null);

        }
    }
}
