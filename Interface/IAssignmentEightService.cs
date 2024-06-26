using Employee_Management_System.DTO;
using Employee_Management_System.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Interface
{
    public interface IAssignmentEightService
    {
        Task<EmployeeAdditionalDetailsFilterCriteria> GetAllEmployeeAdditionalDetailsByPagination(EmployeeAdditionalDetailsFilterCriteria employeeAdditionalDetailsFilterCriteria);
        Task<List<EmployeeBasicAndAdditionalDetailsDTO>> GetAllEmployeeBasicAndAdditionalDetails();
        Task<EmployeeFilterCriteria> GetAllEmployeeBasicDetailsByPagination(EmployeeFilterCriteria employeeFilterCriteria);
        Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionDetailsByBasicUId(string? basicUid);
        Task<SecurityDTO> MakeGetRequestGetSecurityByUId(string uId);
        Task<SecurityDTO> MakePostRequestAddSecurity(SecurityDTO securityDTO);
    }
}
