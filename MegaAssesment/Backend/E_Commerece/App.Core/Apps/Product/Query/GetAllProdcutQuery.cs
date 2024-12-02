using App.Core.Dtos;
using App.Core.Interface;
using Dapper;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Apps.Product.Query
{
    public class GetAllProdcutQuery : IRequest<List<Domain.Product>>
    {
    }
    public class GetAllProductQueryHandller : IRequestHandler<GetAllProdcutQuery, List<Domain.Product>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllProductQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Domain.Product>> Handle(GetAllProdcutQuery request, CancellationToken cancellationToken)
        {
            //var list =await _appDbContext.Set<Domain.Product>().AsTracking().ToListAsync();
            //return list.Adapt<List<ProdcutDto>>();   
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Product Where  IsActive =1";
            var data = await connection.QueryAsync<Domain.Product>(query);
            return data.AsList();

        }
    }
}
