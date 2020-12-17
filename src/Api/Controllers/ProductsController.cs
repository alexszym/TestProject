using Api.Features;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("v1")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("products")]
        [SwaggerOperation("Get list of products")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetList()
        {
            return Ok(await _mediator.Send(new ListProducts.Query()));
        }

        [HttpGet("product/{id}")]
        [SwaggerOperation("Get product by id")]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ProductModel>> Get(long id)
        {
            return await _mediator.Send(new GetProduct.Query() { Id = id });
        }

        [HttpPost("product")]
        [SwaggerOperation("Create a product")]
        public async Task<ActionResult> Create([FromForm]CreateProduct.Command request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("product/{id}")]
        [SwaggerOperation("Update an existing product")]
        public async Task<ActionResult> Put(long id, [FromForm] UpdateProduct.Command request)
        {
            request.Id = id;
            return await _mediator.Send(request);
        }

        [HttpDelete("product/{id}")]
        [SwaggerOperation("Permanently delete a product")]
        public async Task<ActionResult> Delete(long id)
        {
            return await _mediator.Send(new DeleteProduct.Command() { Id = id });
        }
    }
}
