using App.Core.Dtos;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.ProductSales.Query
{
    public class GetInvoiceById : IRequest<List<Domain.SalesMaster>>
    {
        public int id { get; set; }
    }
    public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceById, List<Domain.SalesMaster>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetInvoiceByIdHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Domain.SalesMaster>> Handle(GetInvoiceById request, CancellationToken cancellationToken)
        {
            var salesMaster = await _appDbContext.Set<Domain.SalesMaster>().Where(sm => sm.UserId == request.id).ToListAsync();
            if (salesMaster == null)
            {
                return null;
            }
            //var salesDetails = await _appDbContext.Set<Domain.SalesDetail>().Where(sd => sd.InvoiceId == salesMaster.InvoiceId).ToListAsync();
            return salesMaster;

        }
    }
}
