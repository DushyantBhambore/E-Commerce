using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.CartDetailtable.Query
{
    public class GetCartByidQuery : IRequest<CartResponseModel>
    {
        public int id { get; set; }
    }

    public class GetCartByidQueryHandller : IRequestHandler<GetCartByidQuery,CartResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        public GetCartByidQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public 
    }
}
