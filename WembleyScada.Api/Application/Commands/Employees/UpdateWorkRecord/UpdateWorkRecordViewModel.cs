namespace WembleyScada.Api.Application.Commands.Employees.UpdateWorkRecord;
[DataContract]
public class UpdateWorkRecordViewModel
{
    [DataMember]
    public List<string> EmployeeIds { get; set; }

    public UpdateWorkRecordViewModel(List<string> employeeIds)
    {
        EmployeeIds = employeeIds;
    }
}


