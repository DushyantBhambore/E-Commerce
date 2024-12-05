﻿using App.Core.Interface;
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
    public class GetInvoiceById : IRequest<InvoiceResponseModel>
    {
        public string invoiceId { get; set; }
    }
    public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceById, InvoiceResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public GetInvoiceByIdHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<InvoiceResponseModel> Handle(GetInvoiceById request, CancellationToken cancellationToken)
        {
            var salesMaster = await _appDbContext.Set<Domain.SalesMaster>().FirstOrDefaultAsync(sm => sm.InvoiceId == request.invoiceId);
            if (salesMaster == null)
            {
                return new InvoiceResponseModel(404, "Invoice Id Not Found", null);
            }
            var salesDetails = await _appDbContext.Set<Domain.SalesDetail>().Where(sd => sd.InvoiceId == request.invoiceId).ToListAsync();
            return new InvoiceResponseModel(200, "Invoice Found", salesDetails);

        }
    }
}