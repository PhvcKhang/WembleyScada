

using WembleyScada.Api.Application.Commands.Employees.DeleteWorkRecord;

namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ApiControllerBase
    {
        public EmployeesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeViewModel>> GetEmployees([FromQuery] EmployeesQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            return await SendCommand(command);
        }

        [HttpPost]
        [Route("WorkRecords/{stationId}")]
        public async Task<IActionResult> CreateWorkRecord([FromRoute] string stationId, [FromBody] CreateWorkRecordViewModel workRecords)
        {
            var command = new CreateWorkRecordCommand(stationId, workRecords.EmployeeIds);
            return await SendCommand(command);
        }

        [HttpPut]
        [Route("WorkRecords/{stationId}")]
        public async Task<IActionResult> UpdateWorkRecords([FromRoute] string stationId, [FromBody] UpdateWorkRecordViewModel workRecord)
        {
            var command = new UpdateWorkRecordCommand(stationId, workRecord.EmployeeIds);
            return await SendCommand(command);
        }
        [HttpDelete]
        [Route("{employeeId}")]
        public async Task<IActionResult> DeletePerson([FromRoute] string employeeId)
        {
            var command = new DeleteEmployeeCommand(employeeId);
            return await SendCommand(command);
        }
        [HttpDelete]
        [Route("WorkRecords/{stationId}")]
        public async Task<IActionResult> DeleteWorkRecords([FromRoute] string stationId, [FromBody] DeleteWorkRecordViewModel workRecord)
        {
            var command = new DeleteWorkRecordCommand(stationId, workRecord.EmployeeIds);
            return await SendCommand(command);
        }
    }

}


