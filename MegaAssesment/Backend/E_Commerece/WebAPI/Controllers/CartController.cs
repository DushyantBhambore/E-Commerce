using App.Core.Apps.CartDetailtable.Command;
using App.Core.Apps.CartDetailtable.Query;
using App.Core.Apps.PayementCardTable.Command;
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

        [HttpDelete("DeleteFromCart")]
        public async Task<IActionResult> DeleteFromCart(RemoverCartDto removerCartDto)
        {
            var result = await _mediator.Send(new RemoveCartCoommand { removerCartDto = removerCartDto });
            return Ok(result);
        }

        [HttpDelete("RemoveAllItem")]
        public async Task<IActionResult> RemoveAllItem(RemoverCartDto removerCartDto)
        {
            var result = await _mediator.Send(new RemoveAllietmCommand { removerCartDto = removerCartDto });
            return Ok(result);
        }

        [HttpGet("GetCartById/{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var result = await _mediator.Send(new GetCartByidQuery {UserId = id });
            return Ok(result);
        }

        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart(CartdetailsDto cartdetailsDto)
        {
            var result = await _mediator.Send(new UpdateCartCommand { cartdetailsDto = cartdetailsDto });
            return Ok(result);
        }

        [HttpPost("PayementCard")]
        public async Task<IActionResult> PayementCard(CardValidationDto cardValidationDto)
        {
            var result = await _mediator.Send(new ValidatePaymentCardCommand { cardValidationDto = cardValidationDto });
            return Ok(result);
        }

        [HttpPost("AddAddess")]
        public async Task<IActionResult> PlaceOrder([FromBody] int UserId,string Address)
        {
            var result = await _mediator.Send(new AddOrUpdateAddressCommand { UserId = UserId, Address = Address });
            return Ok(result);
        }

        [HttpPost("AddCard")]
        public async Task<IActionResult> AddCard([FromBody] CardDto card)
        {
            var result = await _mediator.Send(new AddCardCommand { cardDto = card });
            return Ok(result);
        }

       
    }

}
