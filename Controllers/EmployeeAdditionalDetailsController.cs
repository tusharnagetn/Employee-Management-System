using Employee_Management_System.DTO;
using Employee_Management_System.Interface;
using Employee_Management_System.Service;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeAdditionalDetailsController : Controller
    {
        private readonly IEmployeeAdditionalDetailsService _employeeAdditionalDetailsService;

        public EmployeeAdditionalDetailsController(IEmployeeAdditionalDetailsService employeeAdditionalDetailsService)
        {
            _employeeAdditionalDetailsService = employeeAdditionalDetailsService;
        }

        [HttpPut]
        public async Task<IActionResult> Create(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            var toResponse = await _employeeAdditionalDetailsService.Create(employeeAdditionalDetailsDTO);
            return Ok(toResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeAdditionalDetailsDTO employeeAdditionalDetailsDTO)
        {
            var toResponse = await _employeeAdditionalDetailsService.Update(employeeAdditionalDetailsDTO);
            return Ok(toResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string uId)
        {
            var toResponse = await _employeeAdditionalDetailsService.Delete(uId);
            return Ok(toResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUid(string uId)
        {
            var toResponse = await _employeeAdditionalDetailsService.GetByUId(uId);
            return Ok(toResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var toResponse = await _employeeAdditionalDetailsService.GetAll();
            return Ok(toResponse);
        }
    }
}
