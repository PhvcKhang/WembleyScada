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
        public async Task<IEnumerable<StationReferenceViewModel>> GetStationReferences([FromQuery] StationReferencesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPut]
        [Route("{stationId}/{referenceId}")]
        public async Task<IActionResult> UpdateMFCs([FromRoute] string stationId, [FromRoute] string referenceId, [FromBody] List<UpdateMFCViewModel> mFCs)
        {
            var command = new UpdateStationReferenceCommand(referenceId, stationId, mFCs);
            return await SendCommand(command);
        }
    }
}
