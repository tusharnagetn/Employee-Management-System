using AutoMapper;
using Employee_Management_System.Common;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.DTO;
using Employee_Management_System.Entity;
using Employee_Management_System.Interface;

namespace Employee_Management_System.Service
{
    public class EmployeeAdditionalDetailsService : IEmployeeAdditionalDetailsService
    {
        private readonly ICosmoseDBService _ICosmoseDBService;
        private readonly IMapper _Mapper;

        public EmployeeAdditionalDetailsService(ICosmoseDBService iCosmoseDBService, IMapper mapper)
        {
            _ICosmoseDBService = iCosmoseDBService;
            _Mapper = mapper;
        }

        public async Task<EmployeeAdditionalDetailsDTO> Create(EmployeeAdditionalDetailsDTO employeeAdditionalDetails)
        {
            var toAdd = _Mapper.Map<EmployeeAdditionalDetails>(employeeAdditionalDetails);
            toAdd.Initialize(true, Credentials.EmployeeAdditionalDetailsDocumentType, "Tushar");

            EmployeeAdditionalDetails entity = await _ICosmoseDBService.AddEntity(toAdd);

            var toResponse = _Mapper.Map<EmployeeAdditionalDetailsDTO>(entity);

            return toResponse;
        }

        public async Task<string> Delete(string uId)
        {
            var entity = await _ICosmoseDBService.GetEmployeeAdditionalDetailsByUId(uId);
            entity.Active = false;
            entity.Archived = true;

            await _ICosmoseDBService.UpdateEntity(entity);

            entity.Initialize(false, Credentials.EmployeeAdditionalDetailsDocumentType, "Tushar");
            entity.Active = false;
            entity.Archived = true;

            await _ICosmoseDBService.AddEntity(entity);

            return "Deleted Successfully";
        }

        public async Task<List<EmployeeAdditionalDetailsDTO>> GetAll()
        {
            var entities = await _ICosmoseDBService.GetAllEmployeeAdditionalDetails();
            return _Mapper.Map<List<EmployeeAdditionalDetailsDTO>>(entities); 
        }

        public async Task<EmployeeAdditionalDetailsDTO> GetByUId(string uId)
        {
            var entity = await _ICosmoseDBService.GetEmployeeAdditionalDetailsByUId(uId);
            return _Mapper.Map<EmployeeAdditionalDetailsDTO>(entity);
        }

        public async Task<EmployeeAdditionalDetailsDTO> Update(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            var entity = await _ICosmoseDBService.GetEmployeeAdditionalDetailsByUId(employeeAdditionalDetailsDTO.UId);
            entity.Active = false;
            entity.Archived = true;

            await _ICosmoseDBService.UpdateEntity(entity);

            _Mapper.Map(employeeAdditionalDetailsDTO, entity);
            entity.Initialize(false, Credentials.EmployeeAdditionalDetailsDocumentType, "Tushar");

            EmployeeAdditionalDetails updatedEntity = await _ICosmoseDBService.AddEntity(entity);

            return _Mapper.Map<EmployeeAdditionalDetailsDTO>(updatedEntity);
        }
    }
}
