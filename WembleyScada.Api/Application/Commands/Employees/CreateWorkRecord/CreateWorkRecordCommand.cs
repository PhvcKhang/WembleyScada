
namespace WembleyScada.Api.Application.Commands.Employees.Create;

    public class CreateWorkRecordCommand : IRequest<bool>
    {
        public string StationId { get; set; }
        public List<string> EmployeeIds { get; set; }

        public CreateWorkRecordCommand(string stationId, List<string> employeeIds)
        {
            StationId = stationId;
            EmployeeIds = employeeIds;
        }
    }
