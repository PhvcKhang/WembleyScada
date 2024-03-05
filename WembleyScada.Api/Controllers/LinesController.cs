namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinesController : ApiControllerBase
    {
        public LinesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<LineViewModel>> GetAsync([FromQuery] LinesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
