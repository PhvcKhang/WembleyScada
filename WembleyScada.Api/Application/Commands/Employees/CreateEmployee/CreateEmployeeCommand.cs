namespace WembleyScada.Api.Application.Commands.Employees.CreateEmployee;

    [DataContract]
    public class CreateEmployeeCommand : IRequest<bool>
    {
        [DataMember]
        public string EmployeeId { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        public CreateEmployeeCommand(string employeeId, string employeeName)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
        }
    }
