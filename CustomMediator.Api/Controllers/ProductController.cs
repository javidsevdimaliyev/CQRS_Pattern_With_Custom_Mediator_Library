using CustomMediator.Api.Commands.Requests;
using CustomMediator.Api.Queries.Requests;
using CustomMediator.Library.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomMediator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{ProductId}")]
        public IActionResult Get([FromRoute] GetByIdProductQueryRequest request)
            => Ok(mediator.Send(request));


        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateProductCommandRequest request)
           => Ok(mediator.Send(request));

    }
}
