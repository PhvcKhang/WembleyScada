namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ApiControllerBase
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetProducts([FromQuery] ProductsQuery query)
        {
            return await _mediator.Send(query);
        }

    }
}
