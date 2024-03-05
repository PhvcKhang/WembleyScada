namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationReferencesController : ApiControllerBase
    {
        public StationReferencesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<StationReferenceViewModel>> GetDeviceReferences([FromQuery] StationReferencesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
