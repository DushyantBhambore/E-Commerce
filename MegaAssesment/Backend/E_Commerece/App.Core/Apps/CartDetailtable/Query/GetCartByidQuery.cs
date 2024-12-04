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

namespace App.Core.Apps.CartDetailtable.Query
{
    public class GetCartByidQuery : IRequest<CartResponseModel>
    {
        public int UserId { get; set; }
    }
    public class GetCartByidQueryHandller : IRequestHandler<GetCartByidQuery,CartResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public GetCartByidQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async  Task<CartResponseModel> Handle(GetCartByidQuery request, CancellationToken cancellationToken)
        {
            var cart = await _appDbContext.Set<Domain.CartMaster>()
          .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            if (cart == null)
            {
            }
            var cartDetails = await _appDbContext.Set<Domain.CartDetail>()
                .Where(cd => cd.CartId == cart.CardMasterId)
                .Select(cd => new GetCartDto
                {
                    ProductId = cd.ProductId,
                    ProductName = cd.Product.ProductName,
                    Qty = cd.Qty,
                    SellingPrice = cd.Product.SellingPrice,
                    ProductImage =  cd.Product.ProductImage,
                    Total = cd.Qty * cd.Product.SellingPrice,
                    CartDetailsId = cd.CardDetailId,

                })
                .ToListAsync(cancellationToken);
            return new CartResponseModel((int)HttpStatusCode.OK, "Cart found", cartDetails);
        }
    }
}
