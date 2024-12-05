using App.Core.Dtos;
using App.Core.Interface;
using Domain;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Core.Apps.CartDetailtable.Command
{
    public class AddToCartCommand : IRequest<CartResponseModel>
    {
        public CartdetailsDto cartdetailsDto { get; set; }
    }
    public class AddToCartCommandHandller : IRequestHandler<AddToCartCommand, CartResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public AddToCartCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CartResponseModel> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var cartMasterId = await _appDbContext.Set<Domain.CartMaster>()
               .FirstOrDefaultAsync(a => a.UserId == request.cartdetailsDto.UserId);

            

            if (cartMasterId == null)
            {

                cartMasterId = new CartMaster
                {
                    UserId = request.cartdetailsDto.UserId,
                };
                await _appDbContext.Set<Domain.CartMaster>().AddAsync(cartMasterId);
                await _appDbContext.SaveChangesAsync(cancellationToken);
            }

            // Check if the product already exists in the cart
            var existingCartDetail = await _appDbContext.Set<CartDetail>()
                .FirstOrDefaultAsync(cd => cd.CartId == cartMasterId.CardMasterId && cd.ProductId == request.cartdetailsDto.ProductId);

            if (existingCartDetail != null)
            {
                // Update the quantity if the product already exists in the cart
                existingCartDetail.Qty += request.cartdetailsDto.Qty;
                _appDbContext.Set<CartDetail>().Update(existingCartDetail);
            }
            else
            {

            
            
                var addcart = new CartDetail
                {
                    CartId = cartMasterId.CardMasterId,
                    ProductId = request.cartdetailsDto.ProductId,
                    Qty = request.cartdetailsDto.Qty,

                };
                await _appDbContext.Set<Domain.CartDetail>().AddAsync(addcart);

            }
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new CartResponseModel((int)HttpStatusCode.OK, "Prodcut Added into the Card", null);
        }

    
    }
}
