using App.Core.Dtos;
using App.Core.Interface;
using Domain;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.ProductSales.Command
{
    public class PlaceOrderCommand : IRequest<PaymentResponseModel>
    {
        public SalesMasterDto salesMasterDto { get; set; }
    }
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, PaymentResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public PlaceOrderCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PaymentResponseModel> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {

            var salesMaster = new SalesMaster
            {
                InvoiceId = $"ORD-{DateTime.Now:yyMMdd}-{Guid.NewGuid().ToString().Substring(0, 3)}", // Auto-generate Invoice ID
                UserId = request.salesMasterDto.UserId,
                InvoiceDate = DateTime.Now,
                Subtotal = (float)request.salesMasterDto.Items.Sum(i => i.SellingPrice * i.SaleQty),
                DeliveryAddress = request.salesMasterDto.DeliveryAddress,
                DeliveryZipcode = request.salesMasterDto.DeliveryZipcode,
                DeliveryState = request.salesMasterDto.DeliveryState,
                DeliveryCountry = request.salesMasterDto.DeliveryCountry
            };
            await _appDbContext.Set<SalesMaster>().AddAsync(salesMaster);
            await _appDbContext.SaveChangesAsync();

            foreach (var item in request.salesMasterDto.Items)
            {
                var salesDetail = new SalesDetail
                {
                    InvoiceId = salesMaster.InvoiceId,
                    ProductId = item.ProductId,
                    ProductCode = item.ProductCode,
                    SaleQty = item.SaleQty,
                    SellingPrice = item.SellingPrice,
                };
                var product = await _appDbContext.Set<Domain.Product>().FindAsync(item.ProductId);
                if (product == null || product.Stock < item.SaleQty)
                {
                    return new PaymentResponseModel((int)HttpStatusCode.BadRequest, "InSufficeint Stock", null);
                }
                product.Stock -= item.SaleQty;
                await _appDbContext.Set<SalesDetail>().AddAsync(salesDetail);


            }
            await _appDbContext.SaveChangesAsync();
            return new PaymentResponseModel((int)HttpStatusCode.OK, "Invoice Generated ", salesMaster);



        }
    }
}
