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
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Command
{
    public class LoginUserCommand : IRequest<JSonModel>
    {
        public LoginDto LoginDto { get; set; }

    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;
        public LoginUserCommandHandler(IAppDbContext appDbContext, IEmailService emailService, ISmsService smsService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
            _smsService = smsService;
        }

        public async Task<JSonModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {


            var checkuser = await _appDbContext.Set<Domain.User>().Where(x => x.Username == request.LoginDto.Username).FirstOrDefaultAsync();
            if (checkuser == null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Username  and Password is  Invalid", null);
            }
            var checkpassword = BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, checkuser.Password);
            if (!checkpassword)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Username  and Password is  Invalid", null);
            }
            // Generate OTP
            var otp = new Random().Next(100000, 999999).ToString();
            // Store OTP in database or cache with expiration time
            await _appDbContext.Set<Domain.Otp>().AddAsync(new Domain.Otp { Email = checkuser.Email, Username = checkuser.Username, Code = otp, Expiration = DateTime.Now.AddMinutes(5) });
            await _appDbContext.SaveChangesAsync();
            // Send OTP to user's email
            await _emailService.SendEmailAsync(checkuser.Email, "Your OTP Code", $"Your OTP code is {otp}");
            return new JSonModel((int)HttpStatusCode.OK, "Otp is Send Successfully", null);

            //var isSmsSent = await _smsService.SendSmsAsync(checkuser.Mobile, $"Your OTP code is {otp}");
            //if (!isSmsSent)
            //{
            //    return new JSonModel((int)HttpStatusCode.InternalServerError, "Failed to send OTP", null);
            //}

            //return new JSonModel((int)HttpStatusCode.OK, "OTP sent successfully via SMS", null);

        }

    }
}
