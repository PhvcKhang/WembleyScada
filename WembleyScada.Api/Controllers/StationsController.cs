namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ApiControllerBase
    {

        public StationsController(IMediator mediator): base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<StationViewModel>> GetAllStations([FromQuery] StationsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
