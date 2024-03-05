namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferencesController : ApiControllerBase
    {

        public ReferencesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<ReferenceViewModel>> GetReferences([FromQuery] ReferencesQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet]
        [Route("Parameters")]
        public async Task<IEnumerable<ParameterViewModel>> GetParameters([FromQuery] ParametersQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
