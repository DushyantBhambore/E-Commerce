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

namespace App.Core.Apps.ProductSales
{
    public class InvoiceCommand : IRequest<InvoiceResponseModel>
    {
        public int userId { get; set; }
    }
    public class InvoiceCommandHandller : IRequestHandler<InvoiceCommand,InvoiceResponseModel>
    {
        private readonly IAppDbContext _appDbContext;
public InvoiceCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<InvoiceResponseModel> Handle(InvoiceCommand request, CancellationToken cancellationToken)
        {

            var cartMaster = await _appDbContext.Set<Domain.CartMaster>().FirstOrDefaultAsync(c => c.UserId == request.userId);
            if (cartMaster == null)
            {
                return new InvoiceResponseModel((int)HttpStatusCode.BadRequest, "User Id Not Found ",null);
            }
            var cartDetails = await _appDbContext.Set<Domain.CartDetail>().Where(cd => cd.CartId == cartMaster.CardMasterId).ToListAsync();

            if (!cartDetails.Any()) return new InvoiceResponseModel((int)HttpStatusCode.BadRequest, "Cart Master Id Not Found", null);

            var invoiceId = $"ORD-{DateTime.Now.Ticks % 1000000:D6}";

            
            var salesMaster = new SalesMaster
            {
                InvoiceId = invoiceId,
                UserId = request.userId,
                InvoiceDate = DateTime.Now,
                Subtotal = cartDetails.Sum(cd => cd.Qty * cd.Product.SellingPrice),
                DeliveryAddress = "cartMaster.DeliveryAddress",
                DeliveryZipcode = 445566,
                DeliveryState = 1,
                DeliveryCountry = 2
            };
           await _appDbContext.Set<SalesMaster>().AddAsync(salesMaster);
            await _appDbContext.SaveChangesAsync();

            foreach (var cartDetail in cartDetails)
            {
                var salesDetail = new SalesDetail
                {
                    InvoiceId = invoiceId,
                    ProductId = cartDetail.ProductId,
                    ProductCode = cartDetail.Product.ProductCode,
                    SaleQty = cartDetail.Qty,
                    SellingPrice = cartDetail.Product.SellingPrice
                };
                await _appDbContext.Set<SalesDetail>().AddAsync(salesDetail);

                ////var product = await _appDbContext.Set<Domain.Product>().FirstOrDefaultAsync(p => p.ProductId == cartDetail.ProductId);
                //if (product != null) product.Quantity -= cartDetail.Qty;
            }




                throw new NotImplementedException();
        }
    }
}
