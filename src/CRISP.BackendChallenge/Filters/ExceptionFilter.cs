using CRISP.BackendChallenge.Constants;
using CRISP.BackendChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRISP.BackendChallenge.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var response = new ErrorModel
        (
            StatusCodes.Status500InternalServerError,
            ErrorConstants.GenericErrorMessage,
            context.Exception.Message
        );

        context.Result = new ObjectResult(response);
        context.ExceptionHandled = true;
    }
}