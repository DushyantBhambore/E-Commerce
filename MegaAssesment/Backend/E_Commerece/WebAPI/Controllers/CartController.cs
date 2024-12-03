using App.Core.Apps.CartDetailtable.Command;
using App.Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(CartdetailsDto cartdetailsDto)
        {
            var result = await _mediator.Send(new AddToCartCommand { cartdetailsDto = cartdetailsDto });
            return Ok(result);
        }

        //[HttpDelete("ReoveCart")]
        //public async Task<IActionResult>
    }

}
