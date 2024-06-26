using Employee_Management_System.Common;
using Employee_Management_System.DTO;
using Newtonsoft.Json;

namespace Employee_Management_System.Entity
{
    public class EmployeeAdditionalDetails : BaseEntity
    {
        [JsonProperty(PropertyName = "employeeBasicDetailsUId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty(PropertyName = "alternateEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateEmail { get; set; }

        [JsonProperty(PropertyName = "alternateMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateMobile { get; set; }

        [JsonProperty(PropertyName = "workInformation", NullValueHandling = NullValueHandling.Ignore)]
        public WorkInfo_ WorkInformation { get; set; }

        [JsonProperty(PropertyName = "personalDetails", NullValueHandling = NullValueHandling.Ignore)]
        public PersonalDetails_ PersonalDetails { get; set; }

        [JsonProperty(PropertyName = "identityInformation", NullValueHandling = NullValueHandling.Ignore)]
        public IdentityInfo_ IdentityInformation { get; set; }
    }

    public class EmployeeAdditionalDetailsFilterCriteria
    {
        public EmployeeAdditionalDetailsFilterCriteria()
        {
            Filter = new List<FilterCriteria>();

            employeeAdditionalDetailsDTOs = new List<EmployeeAdditionalDetailsDTO>();
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<FilterCriteria> Filter { get; set; }
        public List<EmployeeAdditionalDetailsDTO> employeeAdditionalDetailsDTOs { get; set; }
    }
}
