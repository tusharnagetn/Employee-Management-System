using Employee_Management_System.Common;
using Employee_Management_System.DTO;
using Newtonsoft.Json;

namespace Employee_Management_System.Entity
{
    public class EmployeeBasicDetails : BaseEntity
    {
        [JsonProperty(PropertyName = "salutory", NullValueHandling = NullValueHandling.Ignore)]
        public string Salutory { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "nickName", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "employeeID", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeID { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "reportingManagerUId", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerUId { get; set; }

        [JsonProperty(PropertyName = "reportingManagerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerName { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public Address_ Address { get; set; }
    }

    public class EmployeeFilterCriteria
    {
        public EmployeeFilterCriteria()
        {
            Filter = new List<FilterCriteria>();

            employeeBasicDetailsDTOs = new List<EmployeeBasicDetailsDTO>();
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<FilterCriteria> Filter { get; set; }
        public List<EmployeeBasicDetailsDTO> employeeBasicDetailsDTOs { get; set; }
    }

    public class FilterCriteria
    {
        public FilterCriteria() 
        {
            FieldValue = new List<string>();
        }
        public string FieldName { get; set; }
        public List<string> FieldValue { get; set; }
    }
}
