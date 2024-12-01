using App.Core.Dtos;
using App.Core.Interface;
using Dapper;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.State.Query
{
    public class GetAllStateQuery : IRequest<List<StateDto>>
    {

    }
    public class GetAllStateQueryHandler : IRequestHandler<GetAllStateQuery, List<StateDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetAllStateQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<StateDto>> Handle(GetAllStateQuery request, CancellationToken cancellationToken)
        {
            //var list = await _appDbContext.Set<Domain.State>().AsTracking().ToListAsync();
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM [State]";
            var data = await connection.QueryAsync<StateDto>(query);
            return data.AsList();
        }
    }
}
