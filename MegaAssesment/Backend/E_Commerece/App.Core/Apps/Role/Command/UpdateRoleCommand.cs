using App.Core.Dtos;
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
    public class UpdateRoleCommand : IRequest<JSonModel>
    {
        public RoleDto RoleDto { get; set; }

    }
    public class UpdateRoleCommandHandller : IRequestHandler<UpdateRoleCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;
        public UpdateRoleCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {

            var checkroleid = await _appDbContext.Set<Domain.Role>().Where(x => x.RoleId == request.RoleDto.RoleId && x.IsActive == true).FirstOrDefaultAsync();

            if (checkroleid == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Role not found", null);

            }
            checkroleid.RoleName = request.RoleDto.RoleName;
            await _appDbContext.SaveChangesAsync();
            return new JSonModel((int)HttpStatusCode.OK, "Role updated successfully", checkroleid);
        }
    }
}
