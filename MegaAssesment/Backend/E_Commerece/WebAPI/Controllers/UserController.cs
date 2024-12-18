﻿using App.Core.Apps.User.Command;
using App.Core.Apps.User.Query;
using App.Core.Dtos;
using App.Core.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;


        public UserController(IMediator mediator)
        {
            _mediator = mediator;
    
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromForm]RegisterDto registerDto)
        {
            var result = await _mediator.Send(new AddRegisterCommand { registerDto = registerDto });
            return Ok(result);
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            var result = await _mediator.Send(new LoginUserCommand { LoginDto = loginDto });
            return Ok(result);
        }
        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto verifyOtpDto)
        {
            var result = await _mediator.Send(new VerifyOtpCommand { VerifyOtp = verifyOtpDto });
            return Ok(result);
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUserById( int id )
        {
            var result = await _mediator.Send(new GetUserByIdQuery { id = id });
            return Ok(result);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotEamilDto forgotEamilDto)
        {
            var result = await _mediator.Send(new ForgetPasswordCommand { forgotEamilDto = forgotEamilDto });
            return Ok(result);
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var result = await _mediator.Send(new ChangePasswordCommand { ChangePasswordDto = changePasswordDto });
            return Ok(result);
        }
        [HttpPost("UpdatedProfile")]
        public async Task<IActionResult> UpdateUser([FromForm] RegisterDto registerDto)
        {
            var result = await _mediator.Send(new UpdateUserCommand { registerDto = registerDto });
            return Ok(result);
        }
    }
}
