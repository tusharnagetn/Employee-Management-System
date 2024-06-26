using AutoMapper;
using Employee_Management_System.Common;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.DTO;
using Employee_Management_System.Entity;
using Employee_Management_System.Interface;
using Newtonsoft.Json;

namespace Employee_Management_System.Service
{
    public class AssignmentEightService : IAssignmentEightService
    {
        private readonly ICosmoseDBService _ICosmoseDBService;
        private readonly IMapper _Mapper;

        public AssignmentEightService(ICosmoseDBService iCosmoseDBService, IMapper mapper)
        {
            _ICosmoseDBService = iCosmoseDBService;
            _Mapper = mapper;
        }

        public async Task<EmployeeAdditionalDetailsFilterCriteria> GetAllEmployeeAdditionalDetailsByPagination(EmployeeAdditionalDetailsFilterCriteria criteria)
        {
            EmployeeAdditionalDetailsFilterCriteria toResponse = new EmployeeAdditionalDetailsFilterCriteria();

            var employeesAdditional = _Mapper.Map<List<EmployeeAdditionalDetailsDTO>>(await _ICosmoseDBService.GetAllEmployeeAdditionalDetails());

            toResponse.TotalCount = employeesAdditional.Count();
            toResponse.PageSize = criteria.PageSize;
            toResponse.Page = criteria.Page;

            var isEmployeeStatusFiltter = criteria.Filter.Any(x => x.FieldName == "EmployeeStatus");
            var isBloodGroupFilter = criteria.Filter.Any(x => x.FieldName == "BloodGroup");

            if (isEmployeeStatusFiltter)
            {
                var FieldValue = criteria.Filter.Find(x => x.FieldName == "EmployeeStatus").FieldValue;
                employeesAdditional = employeesAdditional.FindAll(x => FieldValue.Contains(x.WorkInformation.EmployeeStatus));
            }
            if (isBloodGroupFilter)
            {
                var FieldValue = criteria.Filter.Find(x => x.FieldName == "BloodGroup").FieldValue;
                employeesAdditional = employeesAdditional.FindAll(x => FieldValue.Contains(x.PersonalDetails.BloodGroup));
            }

            var skip = criteria.PageSize * (criteria.Page - 1);

            employeesAdditional = employeesAdditional.ToList();

            foreach (var item in employeesAdditional)
            {
                toResponse.employeeAdditionalDetailsDTOs.Add(item);
            }

            return toResponse;
        }

        public async Task<List<EmployeeBasicAndAdditionalDetailsDTO>> GetAllEmployeeBasicAndAdditionalDetails()
        {
            var allEmployeeBasicDetails = await _ICosmoseDBService.GetAllEmployeeBasicDetails();

            var allAdditionalDetails = await _ICosmoseDBService.GetAllEmployeeAdditionalDetails();

            List<EmployeeBasicAndAdditionalDetailsDTO> toResponse = new List<EmployeeBasicAndAdditionalDetailsDTO>();

            foreach (var item in allEmployeeBasicDetails)
            {
                var additionalDetails = allAdditionalDetails.Find(x => x.EmployeeBasicDetailsUId == item.UId);

                if (additionalDetails != null)
                {
                    var toAdd = new EmployeeBasicAndAdditionalDetailsDTO()
                    {
                        BasicDetails = _Mapper.Map<EmployeeBasicDetailsDTO>(item),
                        AdditionDetails = _Mapper.Map<EmployeeAdditionalDetailsDTO>(additionalDetails)
                    };
                    toResponse.Add(toAdd);
                }
            }

            return toResponse;
        }

        public async Task<EmployeeFilterCriteria> GetAllEmployeeBasicDetailsByPagination(EmployeeFilterCriteria criteria)
        {
            EmployeeFilterCriteria toResponse = new EmployeeFilterCriteria();

            var employees = _Mapper.Map<List<EmployeeBasicDetailsDTO>>(await _ICosmoseDBService.GetAllEmployeeBasicDetails());

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

        public async Task<EmployeeAdditionalDetailsDTO> GetEmployeeAdditionDetailsByBasicUId(string? basicUid)
        {
            var allAdditionalDetails = await _ICosmoseDBService.GetAllEmployeeAdditionalDetails();

            var additionalDetail = allAdditionalDetails.Find(x => x.EmployeeBasicDetailsUId == basicUid);

            return _Mapper.Map<EmployeeAdditionalDetailsDTO>(additionalDetail);
        }

        public async Task<SecurityDTO> MakeGetRequestGetSecurityByUId(string uId)
        {
            var requestObj = await HTTPClientHelper.MakeGetRequest(Credentials.BaseUrl, Credentials.GetSecurityByUId + uId);
            return JsonConvert.DeserializeObject<SecurityDTO>(requestObj);
        }

        public async Task<SecurityDTO> MakePostRequestAddSecurity(SecurityDTO securityDTO)
        {
            var serialized = JsonConvert.SerializeObject(securityDTO);
            var requestObj = await HTTPClientHelper.MakePostRequest(Credentials.BaseUrl, Credentials.AddSecurity, serialized);
            return JsonConvert.DeserializeObject<SecurityDTO>(requestObj);
        }
    }
}
