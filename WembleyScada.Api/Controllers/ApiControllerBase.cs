namespace WembleyScada.Api.Controllers
{
    [ApiController]
    public class ApiControllerBase : Controller
    {
        protected readonly IMediator _mediator;

        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> CommandAsync<T>(IRequest<T> request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
