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
    public class GetUserByIdQuery : IRequest<Domain.User>
    {
        public int id { get; set; }
    }
    public class GetUserByIdQueryHandller : IRequestHandler<GetUserByIdQuery, Domain.User>
    {
        private readonly IAppDbContext _appDbContext;

        public GetUserByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Domain.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM [User] Where UserId = @Id And IsActive=1";
            var data = await connection.QueryFirstOrDefaultAsync<Domain.User>(query, new { Id = request.id });
            return data;

        }
    }

}
