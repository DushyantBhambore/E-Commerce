using App.Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Core.Apps.Product.Command
{
    public class DeleteProductCommand : IRequest<string>
    {
        public int id { get; set; }
    }
    public class DeleteProductCommandHandller : IRequestHandler<DeleteProductCommand,string>
    {
        private readonly IAppDbContext _appDbContext;

        public DeleteProductCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var findid = await _appDbContext.Set<Domain.Product>().
                FirstOrDefaultAsync(a => a.ProdcutId == request.id && a.IsActive==true);
            if (findid == null)
            {
                return JsonSerializer.Serialize(new { message = "Product Id is not Found" });
            }
            findid.IsActive = false;
            findid.IsDeletd = true;
            findid.DeletedBy = "Admin";
            findid.DeletedOn = DateTime.Now;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return JsonSerializer.Serialize(new { message = "Product Deleted Successfully" });
        }
    }
}
