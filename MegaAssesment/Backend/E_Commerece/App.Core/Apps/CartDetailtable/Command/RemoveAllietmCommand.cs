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
    public class RemoveAllietmCommand : IRequest<CartResponseModel>
    {
        public RemoverCartDto removerCartDto { get; set; }
    }
    public class RemoveAllietmCommandHYandller : IRequestHandler<RemoveAllietmCommand,CartResponseModel>
    {
        private readonly IAppDbContext _appDbContext;
        public RemoveAllietmCommandHYandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CartResponseModel> Handle(RemoveAllietmCommand request, CancellationToken cancellationToken)
        {
            var productid = await _appDbContext.Set<Domain.CartMaster>().
                FirstOrDefaultAsync(a => a.UserId == request.removerCartDto.UserId);

            if (productid == null)
            {
                return new CartResponseModel((int)HttpStatusCode.BadRequest, "UserId is not Found", null);
            }
            var cartDetail = await _appDbContext.Set<Domain.CartDetail>()
        .FirstOrDefaultAsync(cd => cd.CartId == productid.CardMasterId
        && cd.ProductId == request.removerCartDto.ProductId, cancellationToken);
            if (cartDetail == null)
            {
                return new CartResponseModel((int)HttpStatusCode.BadRequest, "Product not found in the cart", null);
            }


            // Check the Qty and decide to decrement or remove
            if (cartDetail.Qty > 1)
            {
                // Decrement the quantity
                _appDbContext.Set<Domain.CartDetail>().Remove(cartDetail);
            }
            // Save changes to the database
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new CartResponseModel((int)HttpStatusCode.OK, "Prodcut Remove Successfully", null);
        }
    }
}
