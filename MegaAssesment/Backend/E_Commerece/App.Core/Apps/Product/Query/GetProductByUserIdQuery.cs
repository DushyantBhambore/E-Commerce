using App.Core.Interface;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Product.Query
{
    public class GetProductByUserIdQuery : IRequest<List<Domain.Product>>
    {
        public int id { get; set; }
    }
    public class GetProductByUserIdQueryHandller : IRequestHandler<GetProductByUserIdQuery, List<Domain.Product>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetProductByUserIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Domain.Product>> Handle(GetProductByUserIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Product Where IsActive =1 And UserId = @UserId";
            var data = await connection.QueryAsync<Domain.Product>(query, new { UserId = request.id });
            return data.AsList();

        }
    }
}
