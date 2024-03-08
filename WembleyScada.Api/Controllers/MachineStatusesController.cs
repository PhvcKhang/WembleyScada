namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineStatusesController : ApiControllerBase
    {
        public MachineStatusesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<MachineStatusViewModel>> GetMachineStatuses([FromQuery] MachineStatusesQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
