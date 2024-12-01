using App.Core.Apps.User.Command;
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
        private readonly IFileService _fileService;


        public UserController(IMediator mediator, IFileService fileService)
        {
            _mediator = mediator;
            _fileService = fileService;
        }
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterDto registerDto)
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
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            var filePath = await _fileService.SaveFileAsync(file, allowedFileExtensions);
            var fileUrl = $"{Request.Scheme}://{Request.Host}/{filePath.Replace('\\', '/')}";
            return Ok(new { FilePath = filePath, FileUrl = fileUrl });
        }
    }
}
