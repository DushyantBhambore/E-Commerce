using App.Core.Dtos;
using App.Core.Interface;
using Domain;
using Domain.ResponseModel;
using Mapster;
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
    public class PlaceOrderCommand : IRequest<InvoiceResponseModel>
    {
        public SalesMasterDto salesMasterDto { get; set; }

    }
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, InvoiceResponseModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IEmailService _emailService;


        public PlaceOrderCommandHandler(IAppDbContext appDbContext, IEmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public async Task<InvoiceResponseModel> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
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
                    return new InvoiceResponseModel((int)HttpStatusCode.BadRequest, "InSufficeint Stock", null, obj);
                }

                obj = salesDetail;
                product.Stock -= item.SaleQty;
                await _appDbContext.Set<SalesDetail>().AddAsync(salesDetail);
              

            }
            await _appDbContext.SaveChangesAsync();

            var checkuser = await _appDbContext.Set<Domain.User>().
                FirstOrDefaultAsync(a => a.UserId == request.salesMasterDto.UserId);
            StringBuilder emailBody = new StringBuilder();
            emailBody.AppendLine($"Your Order has been Confirmed. Invoice ID: {salesMaster.InvoiceId}");
            emailBody.AppendLine($"Date: {salesMaster.InvoiceDate.ToString("yyyy-MM-dd")}");
            emailBody.AppendLine("Product Details:");

            decimal totalAmount = 0;

            foreach (var item in request.salesMasterDto.Items)
            {
                decimal itemTotal = (decimal)(item.SellingPrice * item.SaleQty);
                emailBody.AppendLine($"Product Code: {item.ProductCode}, Price: {item.SellingPrice:C}, Quantity: {item.SaleQty}, Total: {itemTotal:C}");
                totalAmount += itemTotal;
            }

            emailBody.AppendLine($"Subtotal: {salesMaster.Subtotal:C}");
            emailBody.AppendLine($"Total Amount: {totalAmount:C}");
            emailBody.AppendLine($"Delivery Address: {salesMaster.DeliveryAddress}");

            // Sending the email
            await _emailService.SendEmailAsync(checkuser.Email, "Your Invoice Has Been Sent", emailBody.ToString());


            //await _emailService.SendEmailAsync(checkuser.Email, "Your Invoice Has Sent To Your Registred Email", $"Your Order is Confirm and Your Invoice  is" +
            //    $" {salesMaster.InvoiceId + salesMaster.InvoiceId +
            //    salesMaster.DeliveryCountry + salesMaster.DeliveryZipcode}");

            return new InvoiceResponseModel((int)HttpStatusCode.OK, "Invoice Generated ",salesMaster,obj );



        }
    }
}
