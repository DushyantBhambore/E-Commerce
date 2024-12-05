using App.Core.Dtos;
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

namespace App.Core.Apps.PayementCardTable.Command
{
    public class ValidatePaymentCardCommand : IRequest<PaymentResponseModel>
    {
        public CardValidationDto cardValidationDto { get; set; }
    }
    public class ValidatePaymentCardCommandHanller : IRequestHandler<ValidatePaymentCardCommand, PaymentResponseModel>
    {
        private readonly IAppDbContext _appDbContext;
        public ValidatePaymentCardCommandHanller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PaymentResponseModel> Handle(ValidatePaymentCardCommand request, CancellationToken cancellationToken)
        {
            // Validate payment details
            var validCard = await _appDbContext.Set<Domain.Card>().FirstOrDefaultAsync(card =>
                card.CardNumber == request.cardValidationDto.CardNumber &&
                card.CVV == request.cardValidationDto.CVV);

            if (validCard == null)
            {
                return new PaymentResponseModel((int)HttpStatusCode.BadRequest, "Card Is Not Added", null);
            }
            return new PaymentResponseModel((int)HttpStatusCode.OK, "Card Is Valid", null);
        }
    }
}
