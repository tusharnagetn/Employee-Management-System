using Employee_Management_System.Entity;

namespace Employee_Management_System.DTO
{
    public class EmployeeBasicAndAdditionalDetailsDTO
    {
        public EmployeeBasicDetailsDTO BasicDetails { get; set; }
        public EmployeeAdditionalDetailsDTO AdditionDetails { get; set; }
    }
}
