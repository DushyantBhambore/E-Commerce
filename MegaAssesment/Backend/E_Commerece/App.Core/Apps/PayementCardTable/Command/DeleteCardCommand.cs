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
    public class DeleteCardCommand :IRequest<string>
    {
        public string cardnumber { get; set; }
    }
    public class DeleteCardCommandHandller : IRequestHandler<DeleteCardCommand,string>
    {
        private readonly IAppDbContext _appDbContext;

        public DeleteCardCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
        {

            var  checkcard = await _appDbContext.Set<Domain.Card>().
                FirstOrDefaultAsync(a=>a.CardNumber == request.cardnumber && a.IsActive==true);

            if (checkcard == null)
            {
                return JsonSerializer.Serialize(new { message = "Card is not Present  Please Add it" });
            }

            checkcard.IsActive = false;
            checkcard.IsDeletd = true;
            await _appDbContext.SaveChangesAsync();

            return JsonSerializer.Serialize(new { message = "Card Deleted Successfully " });
        }
    }
}
