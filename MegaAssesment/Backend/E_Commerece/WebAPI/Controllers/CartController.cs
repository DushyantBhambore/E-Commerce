﻿using App.Core.Apps.CartDetailtable.Command;
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

        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment(CardValidationDto cardValidation)
        {
            var result = await _mediator.Send(new ProcessPaymentCommand { cardValidationDto = cardValidation });
            return Ok(result);
        }
    }

}
