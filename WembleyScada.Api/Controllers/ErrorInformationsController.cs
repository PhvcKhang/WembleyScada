namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorInformationsController : ApiControllerBase
    {
        public ErrorInformationsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<ErrorStatusViewModel>> GetErrorStatuses([FromQuery] ErrorStatusesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
