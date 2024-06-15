using Employee_Management_System.DTO;
using Employee_Management_System.Entity;

namespace Employee_Management_System.Interface
{
    public interface IEmployeeBasicDetailsService
    {
        Task<EmployeeBasicDetailsDTO> Create(EmployeeBasicDetailsDTO employeeBasicDetailsDTO);
        Task<EmployeeBasicDetailsDTO> Update(EmployeeBasicDetailsDTO employeeBasicDetailsDTO);
        Task<string> Delete(string uId);
        Task<EmployeeBasicDetailsDTO> GetByUId(string uId);
        Task<List<EmployeeBasicDetailsDTO>> GetAll();
        Task<List<EmployeeDetailsDTO>> GetAllEmployeeDetails();
        Task<List<EmployeeBasicDetailsDTO>> AddListOfEmployeeBasicDetails(List<EmployeeBasicDetailsDTO> employeeBasicDetails);
        Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria criteria);
    }
}
