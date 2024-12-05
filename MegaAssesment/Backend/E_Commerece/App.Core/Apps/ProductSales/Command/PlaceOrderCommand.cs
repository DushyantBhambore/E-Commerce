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
using static System.Net.WebRequestMethods;

namespace App.Core.Apps.ProductSales.Command
{
    public class PlaceOrderCommand : IRequest<PaymentResponseModel>
    {
        public SalesMasterDto salesMasterDto { get; set; }
    }
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, PaymentResponseModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IEmailService _emailService;

        public PlaceOrderCommandHandler(IAppDbContext appDbContext, IEmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public async Task<PaymentResponseModel> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            object obj = "";

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
                obj = salesDetail;


            }
            await _appDbContext.SaveChangesAsync();

            var checkuser = await _appDbContext.Set<Domain.User>().
                FirstOrDefaultAsync(a => a.UserId == request.salesMasterDto.UserId);

            await _emailService.SendEmailAsync(checkuser.Email, "Your Invoice Has Sent To Your Registred Email", $"Your Invoice is {salesMaster.ToString() + obj.ToString()}");

            return new PaymentResponseModel((int)HttpStatusCode.OK, "Invoice Generated ", salesMaster);



        }
    }
}
