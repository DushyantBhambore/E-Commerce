using App.Core.Interface;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.ProductSales.Query
{
    public class GetReciptByidQuery : IRequest<List<Domain.SalesDetail>>
    {
        public string id { get; set; }
    }
    public class GetReciptByidQueryHandller : IRequestHandler<GetReciptByidQuery, List<Domain.SalesDetail>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetReciptByidQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<SalesDetail>> Handle(GetReciptByidQuery request, CancellationToken cancellationToken)
        {
            var salesdetails = await _appDbContext.Set<Domain.SalesDetail>().Where(sm => sm.InvoiceId == request.id).ToListAsync();
            if (salesdetails == null)
            {
                return null;
            }
            return salesdetails;
        }
    }
}
