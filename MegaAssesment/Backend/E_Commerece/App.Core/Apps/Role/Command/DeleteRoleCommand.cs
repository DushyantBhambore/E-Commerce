using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Role.Command
{
    public class DeleteRoleCommand : IRequest<JSonModel>
    {
        public int id { get; set; }
    }
    public class DeleteRoleCommandValidator : IRequestHandler<DeleteRoleCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;
        public DeleteRoleCommandValidator(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {

            var checkroleid = await _appDbContext.Set<Domain.Role>().Where(x => x.RoleId == request.id && x.IsActive == true).FirstOrDefaultAsync();
            if (checkroleid == null)
            {

                return new JSonModel((int)HttpStatusCode.BadRequest, "Role already exists", null);

            }

            checkroleid.IsActive = false;
            checkroleid.IsDeleted = true;
            await _appDbContext.SaveChangesAsync();
                return new JSonModel((int)HttpStatusCode.OK, "Role already exists", checkroleid);


        }
    }
}
