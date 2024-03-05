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
    }
}
