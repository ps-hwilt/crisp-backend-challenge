using CRISP.BackendChallenge.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRISP.BackendChallenge.Filters;

public class EmployeeModelStateFilter : IActionFilter
{
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }
        
        var error = context.ModelState
            .Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage);
        
        context.Result = new ObjectResult(new
        {
            message = "Invalid Model",
            errors = error
        })
        {
            StatusCode = ErrorConstants.BadRequestStatusCode
        };
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}