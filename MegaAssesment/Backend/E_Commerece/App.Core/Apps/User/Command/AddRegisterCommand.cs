﻿using App.Core.Dtos;
using App.Core.Interface;
using Domain.ResponseModel;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.User.Command
{
    public class AddRegisterCommand : IRequest<JSonModel>
    {
        public RegisterDto registerDto { get; set; }

    }
    public class AddRegisterCommandHandler : IRequestHandler<AddRegisterCommand, JSonModel>
    {
        private readonly IAppDbContext _appDbContext;

        private readonly IEmailService _emailService;

        private readonly IFileService _fileservice;
        public AddRegisterCommandHandler(IAppDbContext appDbContext,
            IWebHostEnvironment environment, IEmailService emailService, IFileService fileservice)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
            _fileservice = fileservice;

        }
        public async Task<JSonModel> Handle(AddRegisterCommand request, CancellationToken cancellationToken)
        {

            var checkemail = await _appDbContext.Set<Domain.User>().Where(x => x.Email ==
            request.registerDto.Email).FirstOrDefaultAsync();

            if (checkemail != null)
            {
                return new JSonModel((int)HttpStatusCode.BadRequest, "Email already exists", null);
            }
            var imageFile = request.registerDto.ImageFile;

            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var filePath = await _fileservice.SaveFileAsync(imageFile, allowedFileExtensions);
            var fileUrl = $"https://localhost:7295/Uploads/{Path.GetFileName(filePath)}";


            string formattedDOB = request.registerDto.DOB.ToString("ddMMyy");
            string username = $"EC_{request.registerDto.LastName.
                ToUpper()}{request.registerDto.FirstName.ToUpper()[0]}{formattedDOB}";
            
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string password = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());

            //string password =Password()

            //string encryptedPassword = Encrypt(password);
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            // Create new user
            var user = new Domain.User
            {
                FirstName = request.registerDto.FirstName,
                LastName = request.registerDto.LastName,
                Email = request.registerDto.Email,
                RoleId = request.registerDto.RoleId,
                DOB = request.registerDto.DOB,
                Mobile = request.registerDto.Mobile,
                Address = request.registerDto.Address,
                Zipcode = request.registerDto.Zipcode,
                StateId = request.registerDto.StateId,
                CountryId = request.registerDto.CountryId,
                ImageFile= fileUrl,
                Username = username,
                Password = encryptedPassword,
                IsActive = true,
                IsDeleted = false
            };

            await _appDbContext.Set<Domain.User>().AddAsync(user);
            await _appDbContext.SaveChangesAsync();

           await _emailService.SendEmailAsync(request.registerDto.Email,
                "Welcome to Our Application", $"Hello {request.registerDto.FirstName}" +
                $",\n\nYour account has been created successfully.\n\nUsername: " +
                $"{username}\nPassword: {password}\n\nRegards,\nTeam");

           
            return new JSonModel((int)HttpStatusCode.OK, "User Added Successfully", user);
        }
    }
    }
