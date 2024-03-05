namespace WembleyScada.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftReportsController : ApiControllerBase
    {
        public ShiftReportsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<ShiftReportViewModel>> GetShiftReportsByTime([FromQuery] ShiftReportsQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IEnumerable<ShiftReportDetailViewModel>> GetShiftReportDetails([FromQuery] ShiftReportDetailsQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet]
        [Route("DownloadReport")]
        public async Task<IActionResult> DownLoadExcelReport([FromQuery] DownloadReportsQuery query)
        {
            var file = await _mediator.Send(query);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OEEreport.xlsx");
        }
        [HttpGet]
        [Route("ShortenDetails")]
        public async Task<IEnumerable<ShiftReportDetailViewModel>> GetShiftReportShortenDetails([FromQuery] ShiftReportShortenDetailsQuery query)
        {
            return await _mediator.Send(query);
        }
        [HttpGet]
        [Route("Latest")]
        public async Task<IEnumerable<ShotOEEViewModel>> GetShotOEEViews([FromQuery] ShiftReportLatestDetailsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}
