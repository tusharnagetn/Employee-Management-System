using Employee_Management_System.DTO;

namespace Employee_Management_System.Interface
{
    public interface IEmployeeAdditionalDetailsService
    {
        Task<EmployeeAdditionalDetailsDTO> Create(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO);
        Task<EmployeeAdditionalDetailsDTO> Update(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO);
        Task<string> Delete(string uId);
        Task<EmployeeAdditionalDetailsDTO> GetByUId(string uId);
        Task<List<EmployeeAdditionalDetailsDTO>> GetAll();
    }
}
