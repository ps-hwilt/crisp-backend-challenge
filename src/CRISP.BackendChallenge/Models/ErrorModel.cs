namespace CRISP.BackendChallenge.Models;

public class ErrorModel
{
    public int StatusCode { get; set; }
    public string? Error { get; set; }
    public string? Details { get; set; }

    public ErrorModel(int statusCode, string? error, string? details)
    {
        StatusCode = statusCode;
        Error = error;
        Details = details;
    }
}