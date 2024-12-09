using App.Core.Dtos;
using App.Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Core.Apps.PayementCardTable.Command
{
    public class AddCardCommand : IRequest<string>
    {
        public CardDto  cardDto { get; set; }
    }
    public class AddCardCommandHandller : IRequestHandler<AddCardCommand,string>
    {
        private readonly IAppDbContext _appDbContext;

        public AddCardCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(AddCardCommand request, CancellationToken cancellationToken)
        {


          
            
                var addcard = new Domain.Card
                {
                    CardNumber = request.cardDto.CardNumber,
                    CVV = request.cardDto.CVV,
                    ExpiryDate = request.cardDto.ExpiryDate,
                    CreatedBy = "User",
                    CreatedOn = DateTime.Now,
                    IsActive = true,
                    IsDeletd = false,
                };
                await _appDbContext.Set<Domain.Card>().AddAsync(addcard);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                return JsonSerializer.Serialize(new { message = "Card is Added Successfully " });
            
        }
    }
}
