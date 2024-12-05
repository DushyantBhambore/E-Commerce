using App.Core.Dtos;
using App.Core.Interface;
using Dapper;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Product.Query
{
    public class GetProductByIdQuery : IRequest<Domain.Product>
    {
        public int id { get; set; }
    }
    public class GetProductByIdQueryHandller : IRequestHandler<GetProductByIdQuery,Domain.Product>
    {
        private readonly IAppDbContext _appDbContext;

        public GetProductByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Domain.Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Product Where ProdcutId = @Id And IsActive =1";
            var data = await connection.QueryFirstOrDefaultAsync<Domain.Product>(query, new { Id = request.id });
            return data.Adapt<Domain.Product>();
        }
    }

}
