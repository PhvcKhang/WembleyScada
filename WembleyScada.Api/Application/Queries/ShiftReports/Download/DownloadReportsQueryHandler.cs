namespace WembleyScada.Api.Application.Queries.ShiftReports.Download;

public class DownloadReportsQueryHandler : IRequestHandler<DownloadReportsQuery, byte[]>
{
    private readonly ApplicationDbContext _context;

    public DownloadReportsQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> Handle(DownloadReportsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ShiftReports
            .Where(x => x.StationId == request.StationId
                     && x.Date >= request.StartTime
                     && x.Date <= request.EndTime)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.ShiftNumber)
            .Include(x => x.Shots)
            .AsNoTracking();

        var shiftReports = await queryable.ToListAsync();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=thaiduongstorage;AccountKey=6PGTpI+9M33voc8abMjQw/1JuIVK5x1LVxlWngVVV4LJcgp9ziRfDU4+mtrr+39U4TG5msth95Gy+AStZELbgg==;EndpointSuffix=core.windows.net");
        var containerClient = blobServiceClient.GetBlobContainerClient("oee-container");
        var blobClient = containerClient.GetBlobClient("OEEreport-Wembley.xlsx");

        var stream = await blobClient.OpenReadAsync();
        var package = new ExcelPackage(stream);
        var worksheet = package.Workbook.Worksheets["sheet1"];

        worksheet.Cells["A6"].Value = $"MÃ MÁY: {request.StationId}";
        worksheet.Cells["D6"].Value = $"FROMDATE: {request.StartTime.Day}/{request.StartTime.Month}/{request.StartTime.Year}";
        worksheet.Cells["G6"].Value = $"TODATE: {request.EndTime.Day}/{request.EndTime.Month}/{request.EndTime.Year}";
        worksheet.Cells["A6:I6"].Style.Font.Bold = true;

        worksheet.Cells["I2:I3"].Value = DateTime.Now.Date;

        for (int column = 2; column <= 7; column++)
        {
            int row = 10;
            foreach (var shiftReport in shiftReports)
            {
                var cell = worksheet.Cells[row, column];
                switch (column)
                {
                    case 2:
                        cell.Value = shiftReport.Date;
                        break;
                    case 3:
                        cell.Value = shiftReport.ShiftNumber;
                        break;
                    case 4:
                        cell.Value = shiftReport.OEE;
                        break;
                    case 5:
                        cell.Value = shiftReport.A;
                        break;
                    case 6:
                        cell.Value = shiftReport.P;
                        break;
                    case 7:
                        cell.Value = shiftReport.Q;
                        break;
                }
                row++;
            }
        }

        var streamModified = new MemoryStream();
        package.SaveAs(streamModified);

        byte[] file = streamModified.ToArray();

        return file;
    }
}
