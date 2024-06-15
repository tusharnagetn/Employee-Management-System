using AutoMapper;
using Employee_Management_System.Common;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.DTO;
using Employee_Management_System.Entity;
using Employee_Management_System.Interface;

namespace Employee_Management_System.Service
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsService
    {
        private readonly ICosmoseDBService _ICosmoseDBService;
        private readonly IMapper _Mapper;

        public EmployeeBasicDetailsService(ICosmoseDBService iCosmoseDBService, IMapper mapper)
        {
            _ICosmoseDBService = iCosmoseDBService;
            _Mapper = mapper;
        }

        public async Task<List<EmployeeBasicDetailsDTO>> AddListOfEmployeeBasicDetails(List<EmployeeBasicDetailsDTO> employeeBasicDetails)
        {
            var response = new List<EmployeeBasicDetailsDTO>();

            foreach (var item in employeeBasicDetails)
            {
                var toAdd = _Mapper.Map<EmployeeBasicDetails>(item);
                toAdd.Initialize(true, Credentials.EmployeeBasicDetailsDocumentType, "Tushar");

                EmployeeBasicDetails entity = await _ICosmoseDBService.AddEntity(toAdd);

                var ToAddInList = _Mapper.Map<EmployeeBasicDetailsDTO>(entity);

                response.Add(ToAddInList);
            }

            return response;
        }

        public async Task<EmployeeBasicDetailsDTO> Create(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var toAdd = _Mapper.Map<EmployeeBasicDetails>(employeeBasicDetailsDTO);
            toAdd.Initialize(true, Credentials.EmployeeBasicDetailsDocumentType, "Tushar");

            EmployeeBasicDetails entity = await _ICosmoseDBService.AddEntity(toAdd);

            var toResponse = _Mapper.Map<EmployeeBasicDetailsDTO>(entity);

            return toResponse;
        }

        public async Task<string> Delete(string uId)
        {
            var entity = await _ICosmoseDBService.GetEmployeeBasicDetailsByUId(uId);
            entity.Active = false;
            entity.Archived = true;

            await _ICosmoseDBService.UpdateEntity(entity);

            entity.Initialize(false, Credentials.EmployeeBasicDetailsDocumentType, "Tushar");
            entity.Active = false;
            entity.Archived = true;

            await _ICosmoseDBService.AddEntity(entity);

            return "Deleted Successfully";
        }

        public async Task<List<EmployeeBasicDetailsDTO>> GetAll()
        {
            var entities = await _ICosmoseDBService.GetAllEmployeeBasicDetails();
            return _Mapper.Map<List<EmployeeBasicDetailsDTO>>(entities);
        }

        public async Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria criteria)
        {
            EmployeeFilterCriteria toResponse = new EmployeeFilterCriteria();

            var employees = await GetAll();

            toResponse.TotalCount = employees.Count();
            toResponse.PageSize = criteria.PageSize;
            toResponse.Page = criteria.Page;

            //Filter with list of FieldValue of each FieldName
            var isRoleFilter = criteria.Filter.Any(x => x.FieldName == "Role");
            var isCityFilter = criteria.Filter.Any(x => x.FieldName == "City");
            var isStateFilter = criteria.Filter.Any(x => x.FieldName == "State");

            if (isRoleFilter)
            {
                var FieldValue = criteria.Filter.Find(x => x.FieldName == "Role").FieldValue;
                employees = employees.FindAll(x => FieldValue.Contains(x.Role));
            }
            if (isCityFilter)
            {
                var FieldValue = criteria.Filter.Find(x => x.FieldName == "City").FieldValue;
                employees = employees.FindAll(x => FieldValue.Contains(x.Address.City));
            }
            if (isStateFilter)
            {
                var FieldValue = criteria.Filter.Find(x => x.FieldName == "State").FieldValue;
                employees = employees.FindAll(x => FieldValue.Contains(x.Address.State));
            }

            var skip = criteria.PageSize * (criteria.Page - 1);

            employees = employees.Skip(skip).Take(criteria.PageSize).ToList();

            foreach (var item in employees)
            {
                toResponse.employeeBasicDetailsDTOs.Add(item);
            }

            return toResponse;
        }

        public async Task<List<EmployeeDetailsDTO>> GetAllEmployeeDetails()
        {
            var allEmployeeBasicDetails = await _ICosmoseDBService.GetAllEmployeeBasicDetails();

            var allAdditionalDetails = await _ICosmoseDBService.GetAllEmployeeAdditionalDetails();

            List<EmployeeDetailsDTO> toResponse = new List<EmployeeDetailsDTO>();

            foreach (var item in allEmployeeBasicDetails)
            {
                var additionalDetails = allAdditionalDetails.Find(x => x.EmployeeBasicDetailsUId == item.UId);

                var toAdd = new EmployeeDetailsDTO()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    Mobile = item.Mobile,
                    ReportingManagerName = item.ReportingManagerName,
                    DateOfBirth = additionalDetails.PersonalDetails.DateOfBirth.ToString("dd-MM-yyyy"),
                    DateOfJoining = additionalDetails.WorkInformation.DateOfJoining.ToString("dd-MM-yyyy"),
                };

                toResponse.Add(toAdd);
            }

            return toResponse;
        }

        public async Task<EmployeeBasicDetailsDTO> GetByUId(string uId)
        {
            var entity = await _ICosmoseDBService.GetEmployeeBasicDetailsByUId(uId);
            return _Mapper.Map<EmployeeBasicDetailsDTO>(entity);
        }

        public async Task<EmployeeBasicDetailsDTO> Update(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var entity = await _ICosmoseDBService.GetEmployeeBasicDetailsByUId(employeeBasicDetailsDTO.UId);
            entity.Active = false;
            entity.Archived = true;

            await _ICosmoseDBService.UpdateEntity(entity);

            _Mapper.Map(employeeBasicDetailsDTO, entity);
            entity.Initialize(false, Credentials.EmployeeBasicDetailsDocumentType, "Tushar");

            EmployeeBasicDetails updatedEntity = await _ICosmoseDBService.AddEntity(entity);

            return _Mapper.Map<EmployeeBasicDetailsDTO>(updatedEntity);
        }
    }
}
