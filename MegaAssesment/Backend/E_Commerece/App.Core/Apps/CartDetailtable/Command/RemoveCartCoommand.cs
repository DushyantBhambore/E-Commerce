using App.Core.Dtos;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Core.Apps.CartDetailtable.Command
{
    public class RemoveCartCoommand : IRequest<CartResponseModel>
    {
        public RemoverCartDto removerCartDto { get; set; }
    }
    public class RemoveCartCommandHandller : IRequestHandler<RemoveCartCoommand, CartResponseModel>
    {
        private readonly IAppDbContext _appDbContext;
        public RemoveCartCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CartResponseModel> Handle(RemoveCartCoommand request, CancellationToken cancellationToken)
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
            if (cartDetail != null)
            {
                _appDbContext.Set<Domain.CartDetail>().RemoveRange(cartDetail);
             await   _appDbContext.SaveChangesAsync(cancellationToken);
            }
            return new CartResponseModel((int)HttpStatusCode.OK, "Prodcut Remove Successfully", null);
        }
    }
}
