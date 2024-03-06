namespace WembleyScada.Api.Application.Commands.Employees.DeleteWorkRecord
{
    public class DeleteWorkRecordCommand  : IRequest<bool>
    {
        public string StationId { get; set; }
        public List<string> EmployeeIds { get; set; }

        public DeleteWorkRecordCommand(string stationId, List<string> employeeIds)
        {
            StationId = stationId;
            EmployeeIds = employeeIds;
        }
    }
}
