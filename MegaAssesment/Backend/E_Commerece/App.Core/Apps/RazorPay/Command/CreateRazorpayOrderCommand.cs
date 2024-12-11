using App.Core.Dtos;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.RazorPay.Command
{
    public class CreateRazorpayOrderCommand : IRequest<RazorpayOrderResponseDto>
    {
        public decimal Amount { get; set; }
        //public string Currency { get; set; }
        //public string ReceiptId { get; set; }

    }
    public class CreateRazorpayOrderCommandHandler : IRequestHandler<CreateRazorpayOrderCommand, RazorpayOrderResponseDto>
    {
        private readonly IConfiguration _configuration;

        public CreateRazorpayOrderCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<RazorpayOrderResponseDto> Handle(CreateRazorpayOrderCommand request, CancellationToken cancellationToken)
        {
            // Fetch Razorpay credentials from appsettings
            string keyId = _configuration["Razorpay:KeyId"];
            string keySecret = _configuration["Razorpay:KeySecret"];

            // Initialize Razorpay client
            var client = new Razorpay.Api.RazorpayClient(keyId, keySecret);

            // Create order
            var options = new Dictionary<string, object>
            {
                { "amount", request.Amount * 100 }, // Amount in paise
                { "currency", "INR" },
                { "receipt", "receipt#123" },
                { "payment_capture", 1 } // Auto-capture payment
            };

            var order = client.Order.Create(options);

            // Return response
            var response = new RazorpayOrderResponseDto
            {
                OrderId = order["id"].ToString(),
                PaymentId = null,
                Signature = null
            };

            return Task.FromResult(response);
        }
    }
}
