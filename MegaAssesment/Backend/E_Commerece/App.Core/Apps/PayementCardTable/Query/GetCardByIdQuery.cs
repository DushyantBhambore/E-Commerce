using App.Core.Dtos;
using App.Core.Interface;
using Dapper;
using Domain;
using Domain.ResponseModel;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.PayementCardTable.Query
{
    public class GetCardByIdQuery : IRequest<PaymentResponseModel>
    {
        public int id { get; set; }
    }
    public class GetCardByIdQueryHandller : IRequestHandler<GetCardByIdQuery,PaymentResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public GetCardByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PaymentResponseModel> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _appDbContext.GetConnection();
            //var query = "SELECT * FROM Card Where CardId = @Id  And IsActive = true,";
            //var data = await connection.QueryAsync<CardDto>(query, new { Id = request.id });
            //return data.Adapt<CardDto>();
            var cartQuery = @"
        SELECT * 
        FROM CartMaster 
        WHERE UserId = @UserId"
            ;

            var cart = await connection.QueryFirstOrDefaultAsync<CartMaster>(
        cartQuery,
        new { UserId = request.id });


            if (cart == null)
            {
                return new PaymentResponseModel((int)HttpStatusCode.OK, "UserID is not Found", null);
            }

            // Query to calculate the subtotal
            var subtotalQuery = @"
        SELECT SUM(cd.Qty * p.SellingPrice) 
        FROM CartDetail cd
        INNER JOIN Product p ON cd.ProductId = p.ProductId
        WHERE cd.CartId = @CartId";

            var subtotal = await connection.ExecuteScalarAsync<decimal?>(
                subtotalQuery,
                new { CartId = cart.CardMasterId });

            return  new PaymentResponseModel((int)HttpStatusCode.OK, "Sub Total", subtotal);

        }
    }
}
