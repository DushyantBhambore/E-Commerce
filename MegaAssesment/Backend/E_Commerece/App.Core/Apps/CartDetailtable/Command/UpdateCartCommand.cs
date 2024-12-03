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

namespace App.Core.Apps.CartDetailtable.Command
{
    public class UpdateCartCommand : IRequest<CartResponseModel>
    {
        public CartdetailsDto   cartdetailsDto { get; set; }
    }
    public class UpdateCartCommandHandller : IRequestHandler<UpdateCartCommand, CartResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdateCartCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CartResponseModel> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {

            //var exintngcartid = await _appDbContext.Set<Domain.CartDetail>()
            //    .Where(a => a.CartId == request.cartdetailsDto.CartId && a.ProductId == request.cartdetailsDto.ProductId).FirstOrDefaultAsync();
            //if (exintngcartid != null)
            //{
            //    exintngcartid.ProductId += 1;
            //    exintngcartid.Qty += 1;
            //    await _appDbContext.SaveChangesAsync(cancellationToken);
            //    return new CartResponseModel((int)HttpStatusCode.OK, "Product is Added", exintngcartid);
            //}

            //return new CartResponseModel
            //    ((int)HttpStatusCode.AlreadyReported, "Your Added Product are ", exintngcartid);
            //}

            return null;
        }
    }
    }

