using WembleyScada.Api.Application.Commands.References;
using WembleyScada.Api.Application.Commands.References.CreateLot;

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
        [HttpPost]
        [Route("{referenceName}")]
        public async Task<IActionResult> CreateLot([FromRoute] string referenceName, [FromBody] CreateLotViewModel lot)
        {
            var command = new CreateLotCommand(referenceName, lot.LotCode, lot.LotSize);
            return await SendCommand(command);
        }
        [HttpPut]
        [Route("{referenceName}")]
        public async Task<IActionResult> UpdateLot([FromRoute] string referenceName, [FromBody] UpdateLotViewModel lot)
        {
            var command = new UpdateLotCommand(referenceName, lot.LotCode, lot.LotSize);
            return await SendCommand(command);
        }

        [HttpPut]
        [Route("Parameters/Completed/{referenceName}")]
        public async Task<IActionResult> UpdateParameter([FromRoute] string referenceName)
        {
            var command = new UpdateParameterCommand(referenceName);
            return await SendCommand(command);
        }
    }
}
