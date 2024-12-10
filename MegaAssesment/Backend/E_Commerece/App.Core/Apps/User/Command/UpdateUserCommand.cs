using App.Core.Dtos;
using App.Core.Interface;
using Domain.ResponseModel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Command
{
    public class UpdateUserCommand : IRequest<UserResponseModel>
    {
        public RegisterDto  registerDto { get; set; }
    }
    public class UpdteUserCommandHandller : IRequestHandler<UpdateUserCommand,UserResponseModel>
    {
        private readonly IAppDbContext _appDbContext;

        private readonly IEmailService _emailService;

        private readonly IFileService _fileservice;
        public UpdteUserCommandHandller(IAppDbContext appDbContext,
            IWebHostEnvironment environment, IEmailService emailService, IFileService fileservice)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
            _fileservice = fileservice;

        }

        public async Task<UserResponseModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var finduserid = await _appDbContext.Set<Domain.User>().
                FirstOrDefaultAsync(a => a.UserId == request.registerDto.UserId);

            if(finduserid == null)
            {
                return new UserResponseModel((int)HttpStatusCode.BadRequest, "Invalid User Id ", null);
            }


            var imageFile = request.registerDto.ImageFile;

            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var filePath = await _fileservice.SaveFileAsync(imageFile, allowedFileExtensions);
            var fileUrl = $"https://localhost:7295/Uploads/{Path.GetFileName(filePath)}";

            finduserid.FirstName = request.registerDto.FirstName;
            finduserid.LastName = request.registerDto.LastName;
            finduserid.Zipcode = request.registerDto.Zipcode;
            finduserid.Address = request.registerDto.Address;
            finduserid.Email = request.registerDto.Email;
            finduserid.CountryId = request.registerDto.CountryId;
            finduserid.ImageFile = fileUrl;
            finduserid.Mobile = request.registerDto.Mobile;
            finduserid.DOB = request.registerDto.DOB;   
            finduserid.RoleId = request.registerDto.RoleId;
            finduserid.StateId = request.registerDto.StateId;

            await _appDbContext.SaveChangesAsync();
            await _emailService.SendEmailAsync(finduserid.Email, "Updated Request", "Your Profile is Updated");
            return new UserResponseModel((int)(HttpStatusCode.OK),"User Profile Updated",finduserid);

        }
    }
}
