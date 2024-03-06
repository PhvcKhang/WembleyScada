namespace WembleyScada.Api.Application.Commands.Employees.DeleteWorkRecord
{
    [DataContract]
    public class DeleteWorkRecordViewModel
    {
        [DataMember]
        public List<string> EmployeeIds { get; set; }

        public DeleteWorkRecordViewModel(List<string> employeeIds)
        {
            EmployeeIds = employeeIds;
        }
    }
}
