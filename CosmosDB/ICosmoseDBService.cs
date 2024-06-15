
using Employee_Management_System.Entity;

namespace Employee_Management_System.CosmosDB
{
    public interface ICosmoseDBService
    {
        Task<dynamic> AddEntity(dynamic entity);
        Task<dynamic> UpdateEntity(dynamic entity);

        Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetails();
        Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails();

        Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByUId(string uid);
        Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsByUId(string uid);
    }
}
