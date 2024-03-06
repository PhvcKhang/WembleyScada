namespace WembleyScada.Api.Application.Commands.Employees.CreateWorkRecord
{
    [DataContract]
    public class CreateWorkRecordViewModel
    {
        [DataMember]
        public List<string> EmployeeIds { get; set; }

        public CreateWorkRecordViewModel(List<string> employeeIds)
        {
            EmployeeIds = employeeIds;
        }
    }
}
