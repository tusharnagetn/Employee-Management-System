using Employee_Management_System.Entity;

namespace Employee_Management_System.DTO
{
    public class EmployeeAdditionalDetailsDTO
    {
        public string UId { get; set; }
        public string EmployeeBasicDetailsUId { get; set; }
        public string AlternateEmail { get; set; }
        public string AlternateMobile { get; set; }
        public WorkInfo_ WorkInformation { get; set; }
        public PersonalDetails_ PersonalDetails { get; set; }
        public IdentityInfo_ IdentityInformation { get; set; }
    }
}
