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
    public class AddRoleCommand : IRequest<JSonModel>
    {
        public RoleDto  RoleDto { get; set; }
    }
    public class AddRoleCommandhandller : IRequestHandler<AddRoleCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        public AddRoleCommandhandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<JSonModel> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {

            var checkrole = await _appDbContext.Set<Domain.Role>().Where(x => x.RoleName == request.RoleDto.RoleName).FirstOrDefaultAsync();

            if (checkrole != null)
            {
                return new  JSonModel( (int)HttpStatusCode.BadRequest, "Role already exists" ,null);
            }

            var role = new Domain.Role
            {
                RoleName = request.RoleDto.RoleName,
                IsActive = true,
                IsDeleted = false,
            };
            await _appDbContext.Set<Domain.Role>().AddAsync(role);
            await _appDbContext.SaveChangesAsync();
             return new JSonModel((int)HttpStatusCode.OK, "Role added successfully", role); ;
        }
    }
}
