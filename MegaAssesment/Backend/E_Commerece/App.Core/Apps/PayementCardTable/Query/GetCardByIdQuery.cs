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

namespace App.Core.Apps.PayementCardTable.Query
{
    public class GetCardByIdQuery : IRequest<CardDto>
    {
        public int id { get; set; }
    }
    public class GetCardByIdQueryHandller : IRequestHandler<GetCardByIdQuery,CardDto>
    {
        private readonly IAppDbContext _appDbContext;

        public GetCardByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CardDto> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _appDbContext.GetConnection();
            var query = "SELECT * FROM Card Where CardId = @Id  And IsActive = true,";
            var data = await connection.QueryAsync<CardDto>(query, new { Id = request.id });
            return data.Adapt<CardDto>();
        }
    }
}
