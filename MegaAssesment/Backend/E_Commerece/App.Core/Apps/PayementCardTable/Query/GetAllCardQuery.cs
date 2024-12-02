using App.Core.Dtos;
using App.Core.Interface;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.PayementCardTable.Query
{
    public class GetAllCardQuery :IRequest<List<CardDto>>
    {

    }
    public class GetAllCardQueryHandller : IRequestHandler<GetAllCardQuery,List<CardDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllCardQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<CardDto>> Handle(GetAllCardQuery request, CancellationToken cancellationToken)
        {
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Card Where IsActive = true";
            var data = await connection.QueryAsync<CardDto>(query);
            return data.AsList();
        }
    }
}
