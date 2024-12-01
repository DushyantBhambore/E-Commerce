using App.Core.Dtos;
using App.Core.Interface;
using Dapper;
using Domain.ResponseModel;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Query
{
    public class GetAllCountryQuery : IRequest<List<CountryDto>>
    {
    }
    public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, List<CountryDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetAllCountryQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<CountryDto>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {

            var list = await _appDbContext.Set<Domain.Country>().AsTracking().ToListAsync();

            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Country";
            var data = await connection.QueryAsync<CountryDto>(query);
            return data.AsList();
        }
    }
}
