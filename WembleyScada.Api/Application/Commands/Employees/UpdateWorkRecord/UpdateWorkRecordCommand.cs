namespace WembleyScada.Api.Application.Commands.Employees.UpdateWorkRecord;
public class UpdateWorkRecordCommand : IRequest<bool>
{
    public string StationId { get; set; }
    public List<string> EmployeeIds { get; set; }
    public UpdateWorkRecordCommand(string stationId, List<string> employeeIds)
    {
        StationId = stationId;
        EmployeeIds = employeeIds;
    }
}
