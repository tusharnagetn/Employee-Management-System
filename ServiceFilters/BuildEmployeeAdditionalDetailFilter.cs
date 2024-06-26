using Employee_Management_System.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employee_Management_System.ServiceFilters
{
    public class BuildEmployeeAdditionalDetailFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeAdditionalDetailsFilterCriteria);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            EmployeeAdditionalDetailsFilterCriteria employeeAdditionalDetailsFilterCriteria = (EmployeeAdditionalDetailsFilterCriteria)param.Value;

            var EmployeeStatusFilter = employeeAdditionalDetailsFilterCriteria.Filter.Find(x => x.FieldName == "EmployeeStatus");
            if (EmployeeStatusFilter == null)
            {
                EmployeeStatusFilter = new FilterCriteria();
                EmployeeStatusFilter.FieldName = "EmployeeStatus";
                EmployeeStatusFilter.FieldValue.Add("Active");
                employeeAdditionalDetailsFilterCriteria.Filter.Add(EmployeeStatusFilter);
            }

            employeeAdditionalDetailsFilterCriteria.Filter.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));

            var result = await next();
        }
    }
}
