using Employee_Management_System.DTO;
using Employee_Management_System.Entity;
using Employee_Management_System.Interface;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeBasicDetailsController : Controller
    {
        private readonly IEmployeeBasicDetailsService _employeeBasicDetailsService;
        private readonly System.Drawing.Color blackColors;

        public EmployeeBasicDetailsController(IEmployeeBasicDetailsService employeeBasicDetailsService)
        {
            _employeeBasicDetailsService = employeeBasicDetailsService;
        }

        [HttpPut]
        public async Task<IActionResult> Create(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var toResponse = await _employeeBasicDetailsService.Create(employeeBasicDetailsDTO);
            return Ok(toResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var toResponse = await _employeeBasicDetailsService.Update(employeeBasicDetailsDTO);
            return Ok(toResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string uId)
        {
            var toResponse = await _employeeBasicDetailsService.Delete(uId);
            return Ok(toResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUid(string uId)
        {
            var toResponse = await _employeeBasicDetailsService.GetByUId(uId);
            return Ok(toResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var toResponse = await _employeeBasicDetailsService.GetAll();
            return Ok(toResponse);
        }

        [HttpGet]
        public async Task<IActionResult> ExportEmployeeDetailsExcelFile()
        {
            var allEmployeeDetails = await _employeeBasicDetailsService.GetAllEmployeeDetails();

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
                    workSheet.Column(1).Width = 10;
                    workSheet.Column(2).Width = 15;
                    workSheet.Column(3).Width = 15;
                    workSheet.Column(4).Width = 25;
                    workSheet.Column(5).Width = 15;
                    workSheet.Column(6).Width = 25;
                    workSheet.Column(7).Width = 15;
                    workSheet.Column(8).Width = 15;

                    workSheet.Cells[1, 1].Value = "Sr No.";
                    workSheet.Cells[1, 2].Value = "First Name";
                    workSheet.Cells[1, 3].Value = "Last Name";
                    workSheet.Cells[1, 4].Value = "Email";
                    workSheet.Cells[1, 5].Value = "Phone No";
                    workSheet.Cells[1, 6].Value = "Reporting Manager Name";
                    workSheet.Cells[1, 7].Value = "Date of Birth";
                    workSheet.Cells[1, 8].Value = "Date of Joining";

                    int recordIndex = 2;
                    foreach (var item in allEmployeeDetails)
                    {
                        workSheet.Cells[recordIndex, 1].Value = recordIndex-1;
                        workSheet.Cells[recordIndex, 2].Value = item.FirstName;
                        workSheet.Cells[recordIndex, 3].Value = item.LastName;
                        workSheet.Cells[recordIndex, 4].Value = item.Email;
                        workSheet.Cells[recordIndex, 5].Value = item.Mobile;
                        workSheet.Cells[recordIndex, 6].Value = item.ReportingManagerName;
                        workSheet.Cells[recordIndex, 7].Value = item.DateOfBirth;
                        workSheet.Cells[recordIndex, 8].Value = item.DateOfJoining;
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

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Upload a valid Excel file.");

            var employeeBasicDetails = new List<EmployeeBasicDetailsDTO>();

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;
                        int colCount = worksheet.Dimension.Columns;

                        for (int row = 2; row <= rowCount; row++) 
                        {
                            var item = new EmployeeBasicDetailsDTO
                            {
                                Salutory = worksheet.Cells[row, 1].Value.ToString(),
                                FirstName = worksheet.Cells[row, 2].Value.ToString(),
                                MiddleName = worksheet.Cells[row, 3].Value.ToString(),
                                LastName = worksheet.Cells[row, 4].Value.ToString(),
                                NickName = worksheet.Cells[row, 5].Value.ToString(),
                                Email = worksheet.Cells[row, 6].Value.ToString(),
                                Mobile = worksheet.Cells[row, 7].Value.ToString(),
                                EmployeeID = worksheet.Cells[row, 8].Value.ToString(),
                                Role = worksheet.Cells[row, 9].Value.ToString(),
                                ReportingManagerUId = worksheet.Cells[row, 10].Value.ToString(),
                                ReportingManagerName = worksheet.Cells[row, 11].Value.ToString(),
                                Address = new Address_()
                                {
                                    AddressLine1 =  worksheet.Cells[row, 12].Value.ToString(),
                                    AddressLine2 =  worksheet.Cells[row, 13].Value.ToString(),
                                    City =  worksheet.Cells[row, 14].Value.ToString(),
                                    State =  worksheet.Cells[row, 15].Value.ToString(),
                                    Zipcode = worksheet.Cells[row, 16].Value.ToString(),
                                    Country = worksheet.Cells[row, 17].Value.ToString(),
                                }
                            };

                            employeeBasicDetails.Add(item);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString);
            }

            if (employeeBasicDetails == null || employeeBasicDetails.Count == 0)
            {
                return BadRequest("No employees provided.");
            }

            var toResponse = await _employeeBasicDetailsService.AddListOfEmployeeBasicDetails(employeeBasicDetails);

            return Ok(toResponse);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllEmployeeByPagination(EmployeeFilterCriteria criteria)
        {
            var toResponse = await _employeeBasicDetailsService.GetAllEmployeeByPagination(criteria);
            return Ok(toResponse);
        }
    }
}
