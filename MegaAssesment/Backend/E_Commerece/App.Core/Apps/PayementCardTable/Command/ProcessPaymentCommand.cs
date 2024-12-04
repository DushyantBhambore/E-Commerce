using App.Core.Dtos;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.PayementCardTable.Command
{
    public class ProcessPaymentCommand : IRequest<PaymentResponseModel>
    {
        public CardValidationDto cardValidationDto { get; set; }
    }
    public class ProcessPaymentCommandHandller : IRequestHandler<ProcessPaymentCommand, PaymentResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public ProcessPaymentCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<PaymentResponseModel> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {

            var cart = await _appDbContext.Set<Domain.CartMaster>()
           .FirstOrDefaultAsync(c => c.UserId == request.cardValidationDto.UserId);

            if (cart == null)
            {
                return new PaymentResponseModel((int)HttpStatusCode.BadRequest, "Invalid Payment Id", null);
            }
            // Validate payment details
            var validCard = await _appDbContext.Set<Domain.Card>().FirstOrDefaultAsync(card =>
                card.CardNumber == request.cardValidationDto.CardNumber &&
                card.ExpiryDate == request.cardValidationDto.ExpiryDate &&
                card.CVV == request.cardValidationDto.CVV);

            if (validCard == null)
            {
                return new PaymentResponseModel((int)HttpStatusCode.BadRequest, "Card Is Not Added", null);
            }

            var cartDetails = await _appDbContext.Set<Domain.CartDetail>().Where(cd => cd.CartId == cart.CardMasterId).ToListAsync(cancellationToken);
            foreach (var item in cartDetails)
            {
                var product = await _appDbContext.Set<Domain.Product>().FindAsync(item.ProductId);
                if (product == null) continue;

                product.Stock -= item.Qty;
                _appDbContext.Set<Domain.Product>().Update(product);
            }

            _appDbContext.Set<Domain.CartDetail>().RemoveRange(cartDetails);
            _appDbContext.Set<Domain.CartMaster>().Remove(cart)
    ;
            await _appDbContext.SaveChangesAsync();

            return new PaymentResponseModel((int)HttpStatusCode.OK, "Payment Successfull", cartDetails);


        }
    }

}
