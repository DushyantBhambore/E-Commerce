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

namespace App.Core.Apps.User.Query
{
    public class GetUserByIdQuery : IRequest<RegisterDto>
    {
        public int id { get; set; }
    }
    public class GetUserByIdQueryHandller : IRequestHandler<GetUserByIdQuery, RegisterDto>
    {
        private readonly IAppDbContext _appDbContext;

        public GetUserByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<RegisterDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM [User] Where UserId = @Id And IsActive = true,";
            var data = await connection.QueryAsync<RegisterDto>(query, new { Id = request.id });
            return data.Adapt<RegisterDto>();
        }
    }

}
