using App.Core.Dtos;
using App.Core.Interface;
using Domain;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

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
            emailBody.AppendLine($@" <html> <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0;'> <table width='100%' cellpadding='0' cellspacing='0' style='max-width: 600px; margin: auto; padding: 20px; background-color: #ffffff; border-radius: 10px; box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);'>
<tr> <td align='center' style='background-color: #2E3B4E; padding: 20px; color: white; font-size: 24px; font-weight: bold; border-top-left-radius: 10px; border-top-right-radius: 10px;'>
<h1 style='margin: 0; color: white;'>EComApplication</h1> </td> </tr> <tr>
<td style='padding: 40px;'> <p style='font-size: 18px; color: #333;'>Dear {checkuser.FirstName},</p> <p style='font-size: 16px; color: #333;'>Congratulations! Your order has been successfully placed on <strong>EComApplication</strong>. Here are your order details:</p> <p style='font-size: 16px; color: #333;'><strong>Invoice ID:</strong> {salesMaster.InvoiceId}</p> 
<p style='font-size: 16px; color: #333;'><strong>Date:</strong> {salesMaster.InvoiceDate:yyyy-MM-dd}</p> <p style='font-size: 16px; color: #333;'><strong>Delivery Address:</strong> {salesMaster.DeliveryAddress}, {salesMaster.DeliveryZipcode}, {salesMaster.DeliveryState}, {salesMaster.DeliveryCountry}</p> <h4 style='font-size: 16px; color: #333;'>Product Details:</h4> <ul style='padding-left: 20px;'>"); decimal totalAmount = 0; foreach (var item in request.salesMasterDto.Items) { decimal itemTotal = (decimal)(item.SellingPrice * item.SaleQty); emailBody.AppendLine($@" <li style='font-size: 16px; color: #333;'> Product Code: {item.ProductCode}, Price: {item.SellingPrice:C}, Quantity: {item.SaleQty}, Total: {itemTotal:C} </li>"); totalAmount += itemTotal; }
            emailBody.AppendLine($@" </ul> <p style='font-size: 16px; color: #333;'><strong>Subtotal:</strong> {salesMaster.Subtotal:C}</p> <p style='font-size: 16px; color: #333;'><strong>Total Amount:</strong> {totalAmount:C}</p> <p style='font-size: 16px; color: #333;'>Thank you for choosing <strong>EComApplication</strong>. We look forward to serving you again!</p> <p style='font-size: 16px; color: #333;'>Best regards,<br>The EComApplication Team</p> </td> </tr> <tr> <td align='center' style='background-color: #2E3B4E; padding: 20px; color: white; font-size: 14px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px;'> <p>© {DateTime.Now.Year} EComApplication. All rights reserved.</p> </td> </tr> </table> </body> </html>"); // Sending the email await _emailService.SendEmailAsync(checkuser.Email, "Your Invoice Has Been Sent", emailBody.ToString());


            //await _emailService.SendEmailAsync(checkuser.Email, "Your Invoice Has Sent To Your Registred Email", $"Your Order is Confirm and Your Invoice  is" +
            //    $" {salesMaster.InvoiceId + salesMaster.InvoiceId +
            //    salesMaster.DeliveryCountry + salesMaster.DeliveryZipcode}");

            return new InvoiceResponseModel((int)HttpStatusCode.OK, "Invoice Generated ", salesMaster, obj);



        }
    }
}
