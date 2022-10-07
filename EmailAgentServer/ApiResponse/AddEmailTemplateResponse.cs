namespace EmailAgentServer.ApiResponse;

public class AddEmailTemplateResponse
{
    public bool IsSuccess { get; set; }
    
    public string? FailedReason { get; set; }
    
    /// <summary>
    /// Missing Parameters given by the request
    /// </summary>
    public string[]? MissingParameters { get; set; }
    
    /// <summary>
    /// Missing place holders in file
    /// </summary>
    public string[]? MissingPlaceHolder { get; set; }
}