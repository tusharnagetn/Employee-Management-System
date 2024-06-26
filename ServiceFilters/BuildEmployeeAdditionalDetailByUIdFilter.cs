using Employee_Management_System.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employee_Management_System.ServiceFilters
{
    public class BuildEmployeeAdditionalDetailByUIdFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments;

            if (!param.ContainsKey("basicUid") || param["basicUid"] == null || string.IsNullOrEmpty(param["basicUid"] as string))
            {
                context.ActionArguments["basicUid"] = "87095948-c720-4b6b-966c-2e9313a5e1ff";
            }

            var result = await next();
        }
    }
}
