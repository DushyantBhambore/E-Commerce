using App.Core.Dtos;
using App.Core.Interface;
using Dapper;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Role.Query
{
    public class GetRoleByIdQuery : IRequest<List<RoleDto>>
    {
        public int id { get; set; }
    }
    public class GetRoleByIdQueryHandller : IRequestHandler<GetRoleByIdQuery, List<RoleDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetRoleByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            //var list = await _appDbContext.Set<Domain.State>().Where(x => x.CountryId == request.id).AsTracking().ToListAsync();
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Role Where RoleId = @Id";
            var data = await connection.QueryAsync<RoleDto>(query, new { Id = request.id });
            return data.AsList();

        }
    }
}
