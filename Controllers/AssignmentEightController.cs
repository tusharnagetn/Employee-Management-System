using Employee_Management_System.DTO;
using Employee_Management_System.Entity;
using Employee_Management_System.Interface;
using Employee_Management_System.ServiceFilters;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AssignmentEightController : Controller
    {
        private readonly IAssignmentEightService _assignmentEightService;

        public AssignmentEightController(IAssignmentEightService assignmentEightService)
        {
            _assignmentEightService = assignmentEightService;
        }

        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeBasicDetailFilter))]
        public async Task<IActionResult> GetAllEmployeeBasicDetailsByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            var toResponse = await _assignmentEightService.GetAllEmployeeBasicDetailsByPagination(employeeFilterCriteria);
            return Ok(toResponse);
        }

        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeAdditionalDetailFilter))]
        public async Task<IActionResult> GetAllEmployeeAdditionalDetailsByPagination(EmployeeAdditionalDetailsFilterCriteria employeeAdditionalDetailsFilterCriteria)
        {
            var toResponse = await _assignmentEightService.GetAllEmployeeAdditionalDetailsByPagination(employeeAdditionalDetailsFilterCriteria);
            return Ok(toResponse);
        }

        [HttpGet("{UId}")]
        public async Task<IActionResult> MakeGetRequestGetSecurityByUId(string UId)
        {
            var toResponse = await _assignmentEightService.MakeGetRequestGetSecurityByUId(UId);
            return Ok(toResponse);
        }

        [HttpPost]
        public async Task<IActionResult> MakePostRequestAddSecurity(SecurityDTO securityDTO)
        {
            var toResponse = await _assignmentEightService.MakePostRequestAddSecurity(securityDTO);
            return Ok(toResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeBasicAndAdditionalDetailsInExcel()
        {
            var AllEmployeeDetails = await _assignmentEightService.GetAllEmployeeBasicAndAdditionalDetails();

            if (AllEmployeeDetails == null || AllEmployeeDetails.Count == 0)
            {
                return StatusCode(404, "Data not found");
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                var stream = new MemoryStream();

                using (var package = new ExcelPackage(stream))
                {
                    var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                    workSheet.Row(1).Height = 20;
                    workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    workSheet.Row(1).Style.Font.Bold = true;
                    workSheet.Row(1).Style.WrapText = true;

                    for (int i = 1; i <= 39; i++) 
                    {
                        workSheet.Column(i).Width = 20;
                    }

                    workSheet.Cells[1, 1].Value = "Sr No.";
                    workSheet.Cells[1, 2].Value = "Salutory";
                    workSheet.Cells[1, 3].Value = "First Name";
                    workSheet.Cells[1, 4].Value = "Middle Name";
                    workSheet.Cells[1, 5].Value = "Last Name";
                    workSheet.Cells[1, 6].Value = "Nick Name";
                    workSheet.Cells[1, 7].Value = "Email";
                    workSheet.Cells[1, 8].Value = "Mobile";
                    workSheet.Cells[1, 9].Value = "Employee Id";
                    workSheet.Cells[1, 10].Value = "Role";
                    workSheet.Cells[1, 11].Value = "Reporting Manager Name";
                    workSheet.Cells[1, 12].Value = "1st Line Address";
                    workSheet.Cells[1, 13].Value = "2nd Line Address";
                    workSheet.Cells[1, 14].Value = "City";
                    workSheet.Cells[1, 15].Value = "State";
                    workSheet.Cells[1, 16].Value = "Country";
                    workSheet.Cells[1, 17].Value = "Zipcode";
                    workSheet.Cells[1, 18].Value = "Alternative Mobile";
                    workSheet.Cells[1, 19].Value = "Alternative Email";
                    workSheet.Cells[1, 20].Value = "Designation Name";
                    workSheet.Cells[1, 21].Value = "Department Name";
                    workSheet.Cells[1, 22].Value = "Location Name";
                    workSheet.Cells[1, 23].Value = "Employee Status";
                    workSheet.Cells[1, 24].Value = "Source Of Hire";
                    workSheet.Cells[1, 25].Value = "Joining Date";
                    workSheet.Cells[1, 26].Value = "Date Of Birth";
                    workSheet.Cells[1, 27].Value = "Age";
                    workSheet.Cells[1, 28].Value = "Gender";
                    workSheet.Cells[1, 29].Value = "Religion";
                    workSheet.Cells[1, 30].Value = "Caste";
                    workSheet.Cells[1, 31].Value = "Maritial Status";
                    workSheet.Cells[1, 32].Value = "Blood Group";
                    workSheet.Cells[1, 33].Value = "Height";
                    workSheet.Cells[1, 34].Value = "Weight";
                    workSheet.Cells[1, 35].Value = "Pan";
                    workSheet.Cells[1, 36].Value = "Aadhar";
                    workSheet.Cells[1, 37].Value = "Nationality";
                    workSheet.Cells[1, 38].Value = "Passport Number";
                    workSheet.Cells[1, 39].Value = "PF Number";

                    int recordIndex = 2;
                    foreach (var item in AllEmployeeDetails)
                    {
                        workSheet.Cells[recordIndex, 1].Value = recordIndex - 1;
                        workSheet.Cells[recordIndex, 2].Value = item.BasicDetails.Salutory;
                        workSheet.Cells[recordIndex, 3].Value = item.BasicDetails.FirstName;
                        workSheet.Cells[recordIndex, 4].Value = item.BasicDetails.MiddleName;
                        workSheet.Cells[recordIndex, 5].Value = item.BasicDetails.LastName;
                        workSheet.Cells[recordIndex, 6].Value = item.BasicDetails.NickName;
                        workSheet.Cells[recordIndex, 7].Value = item.BasicDetails.Email;
                        workSheet.Cells[recordIndex, 8].Value = item.BasicDetails.Mobile;
                        workSheet.Cells[recordIndex, 9].Value = item.BasicDetails.EmployeeID;
                        workSheet.Cells[recordIndex, 10].Value = item.BasicDetails.Role;
                        workSheet.Cells[recordIndex, 11].Value = item.BasicDetails.ReportingManagerName;
                        workSheet.Cells[recordIndex, 12].Value = item.BasicDetails.Address.AddressLine1;
                        workSheet.Cells[recordIndex, 13].Value = item.BasicDetails.Address.AddressLine2;
                        workSheet.Cells[recordIndex, 14].Value = item.BasicDetails.Address.City;
                        workSheet.Cells[recordIndex, 15].Value = item.BasicDetails.Address.State;
                        workSheet.Cells[recordIndex, 16].Value = item.BasicDetails.Address.Country;
                        workSheet.Cells[recordIndex, 17].Value = item.BasicDetails.Address.Zipcode;
                        workSheet.Cells[recordIndex, 18].Value = item.AdditionDetails.AlternateMobile;
                        workSheet.Cells[recordIndex, 19].Value = item.AdditionDetails.AlternateEmail;
                        workSheet.Cells[recordIndex, 20].Value = item.AdditionDetails.WorkInformation.DesignationName;
                        workSheet.Cells[recordIndex, 21].Value = item.AdditionDetails.WorkInformation.DepartmentName;
                        workSheet.Cells[recordIndex, 22].Value = item.AdditionDetails.WorkInformation.LocationName;
                        workSheet.Cells[recordIndex, 23].Value = item.AdditionDetails.WorkInformation.EmployeeStatus;
                        workSheet.Cells[recordIndex, 24].Value = item.AdditionDetails.WorkInformation.SourceOfHire;
                        workSheet.Cells[recordIndex, 25].Value = item.AdditionDetails.WorkInformation.DateOfJoining;
                        workSheet.Cells[recordIndex, 26].Value = item.AdditionDetails.PersonalDetails.DateOfBirth;
                        workSheet.Cells[recordIndex, 27].Value = item.AdditionDetails.PersonalDetails.Age;
                        workSheet.Cells[recordIndex, 28].Value = item.AdditionDetails.PersonalDetails.Gender;
                        workSheet.Cells[recordIndex, 29].Value = item.AdditionDetails.PersonalDetails.Religion;
                        workSheet.Cells[recordIndex, 30].Value = item.AdditionDetails.PersonalDetails.Caste;
                        workSheet.Cells[recordIndex, 31].Value = item.AdditionDetails.PersonalDetails.MaritalStatus;
                        workSheet.Cells[recordIndex, 32].Value = item.AdditionDetails.PersonalDetails.BloodGroup;
                        workSheet.Cells[recordIndex, 33].Value = item.AdditionDetails.PersonalDetails.Height;
                        workSheet.Cells[recordIndex, 34].Value = item.AdditionDetails.PersonalDetails.Weight;
                        workSheet.Cells[recordIndex, 35].Value = item.AdditionDetails.IdentityInformation.PAN;
                        workSheet.Cells[recordIndex, 36].Value = item.AdditionDetails.IdentityInformation.Aadhar;
                        workSheet.Cells[recordIndex, 37].Value = item.AdditionDetails.IdentityInformation.Nationality;
                        workSheet.Cells[recordIndex, 38].Value = item.AdditionDetails.IdentityInformation.PassportNumber;
                        workSheet.Cells[recordIndex, 39].Value = item.AdditionDetails.IdentityInformation.PFNumber;

                        recordIndex++;
                    }
                    package.Save();

                    stream.Position = 0;
                    string excelName = "Employees.xlsx";
                    return File(stream, "application/octet-stream", excelName);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(BuildEmployeeAdditionalDetailByUIdFilter))]
        public async Task<IActionResult> GetEmployeeAdditionDetailsByBasicUId(string? basicUid)
        {
            var toResponse = await _assignmentEightService.GetEmployeeAdditionDetailsByBasicUId(basicUid);
            return Ok(toResponse);
        }
    }
}
