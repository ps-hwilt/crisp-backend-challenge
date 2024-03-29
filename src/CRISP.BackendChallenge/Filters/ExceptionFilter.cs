using CRISP.BackendChallenge.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRISP.BackendChallenge.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new
        {
            error = ErrorConstants.GenericErrorMessage
        })
        {
            StatusCode = ErrorConstants.InternalServiceStatusCode
        };

        context.ExceptionHandled = true;
    }
}