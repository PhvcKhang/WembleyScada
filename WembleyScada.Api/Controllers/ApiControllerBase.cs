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

        protected async Task<IActionResult> SendCommand<T>(IRequest<T> request)
        {
            try
            {
                var response = await _mediator.Send(request);
                return Ok(response);
            }
            catch (ResourceNotFoundException ex)
            {
                var errorMessage = new ErrorMessage(ex);
                return NotFound(errorMessage);
            }
            catch (Exception ex)
            {
                var errorMessage = new ErrorMessage(ex);
                return BadRequest(errorMessage);
            }

        }
    }
}
