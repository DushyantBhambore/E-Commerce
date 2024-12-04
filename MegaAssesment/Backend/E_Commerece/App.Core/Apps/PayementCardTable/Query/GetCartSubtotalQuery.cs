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

namespace App.Core.Apps.PayementCardTable.Query
{
    public class GetCartSubtotalQuery : IRequest<PaymentResponseModel>
    {
        public int UserId { get; set; }
    }
    public class GetCartSubtotalQueryHandller : IRequestHandler<GetCartSubtotalQuery, PaymentResponseModel>
    {

        private readonly IAppDbContext _appDbContext;

        public GetCartSubtotalQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PaymentResponseModel> Handle(GetCartSubtotalQuery request, CancellationToken cancellationToken)
        {

            var cart = await _appDbContext.Set<Domain.CartMaster>()
          .FirstOrDefaultAsync(c => c.UserId == request.UserId, cancellationToken);

            if (cart == null)
            {
                return new PaymentResponseModel((int)HttpStatusCode.OK, "UserID is not Found", null);
            }
            var subtotal = await _appDbContext.Set<Domain.CartDetail>()
                .Where(cd => cd.CartId == cart.CardMasterId)
                .SumAsync(cd => cd.Qty * cd.Product.SellingPrice, cancellationToken);
            return new PaymentResponseModel((int)HttpStatusCode.OK,"Sub Total",null);
        }
    }
}
