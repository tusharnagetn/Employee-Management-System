using Employee_Management_System.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employee_Management_System.ServiceFilters
{
    public class BuildEmployeeBasicDetailFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeFilterCriteria);
            if(param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            EmployeeFilterCriteria employeeFilterCriteria = (EmployeeFilterCriteria)param.Value;
            var stateFilter = employeeFilterCriteria.Filter.Find(x => x.FieldName == "State");
            if(stateFilter == null)
            {
                stateFilter = new FilterCriteria();
                stateFilter.FieldName = "State";
                stateFilter.FieldValue.Add("MH");
                stateFilter.FieldValue.Add("MP");
                employeeFilterCriteria.Filter.Add(stateFilter);
            }

            employeeFilterCriteria.Filter.RemoveAll(a => string.IsNullOrEmpty(a.FieldName));

            var result = await next();
        }
    }
}
