using App.Core.Apps.Product.Command;
using App.Core.Apps.Product.Query;
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
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProdcut([FromForm] ProdcutDto prodcutDto)
        {
            var result = await _mediator.Send(new AddProductCommad { prodcutDto = prodcutDto });
            return Ok(result);
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _mediator.Send(new GetAllProdcutQuery() );
            return Ok(result);
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProdcutDto prodcutDto)
        {
            var result = await _mediator.Send(new UpdateProductCommand { prodcutDto = prodcutDto });
            return Ok(result);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { id = id });
            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductByid(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery { id = id });
            return Ok(result);
        }

    }
}
