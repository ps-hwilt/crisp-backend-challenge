namespace CRISP.BackendChallenge.Constants;

public class ErrorConstants
{
    public const string EmployeesNotFound = "No Employees Found in Database";
    public const string EmployeeNotFound = "Employee Not Found";
    public const string GenericErrorMessage = "An error occurred while processing your request";
    public const int BadRequestStatusCode = StatusCodes.Status400BadRequest;
    public const int InternalServiceStatusCode = StatusCodes.Status500InternalServerError;
}