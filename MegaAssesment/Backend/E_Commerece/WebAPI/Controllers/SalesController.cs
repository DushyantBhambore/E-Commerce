using App.Core.Apps.ProductSales;
using App.Core.Apps.ProductSales.Command;
using App.Core.Apps.ProductSales.Query;
using App.Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromBody] SalesMasterDto command)
        {

            var result = await _mediator.Send(new PlaceOrderCommand { salesMasterDto = command });
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetInvoceBYId(int id)
        {
            var result = await _mediator.Send(new GetInvoiceById{id =id});
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetReciptByidQuery(string id)
        {
            var result = await _mediator.Send(new GetReciptByidQuery { id =  id});
            return Ok(result);
        }
    }
}
