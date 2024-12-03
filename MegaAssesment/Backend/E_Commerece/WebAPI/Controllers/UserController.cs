using App.Core.Apps.User.Command;
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
        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto verifyOtpDto)
        {
            var result = await _mediator.Send(new VerifyOtpCommand { VerifyOtp = verifyOtpDto });
            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserById([FromQuery] int id )
        {
            var result = await _mediator.Send(new GetUserByIdQuery { id = id });
            return Ok(result);
        }
    
    }
}
