using EmailAgentServer.DBEntity;
using EmailAgentServer.Entity;
using EmailAgentServer.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailAgentServer.Controllers;

[Route("/Application")]
[ApiController]
public class ApplicationController : ControllerBase
{
    private IApplicationService _applicationService;
    
    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [Authorize(Roles = "User")]
    [HttpPost("Application")]
    public IActionResult CreateApplication(string applicationName)
    {
        var createResult = _applicationService.CreateApplication(applicationName);

        return Ok(new
        {
            result = createResult? "success":"failed",
        });
    }

    [Authorize(Roles = "User")]
    [HttpGet("Application")]
    public ActionResult<IEnumerable<Application>> GetApplicationList()
    {
        return _applicationService.GetApplicationList().ToList();
    }

    /// <summary>
    /// Create a token for application
    /// </summary>
    /// <param name="unit">
    /// Unit of time
    /// 1:Day 2:Month 3:Year
    /// </param>
    /// <param name="validTime">
    /// Length of valid time
    /// </param>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    [Authorize(Roles = "User")]
    [HttpPost("token")]
    public ActionResult<ApplicationCreateTokenEntity> CreateApplicationToken(int unit, int validTime, int applicationId)
    {
        var expireDate = DateTime.Now;
        switch (unit)
        {
            case 1: // Day
                expireDate = expireDate.AddDays(validTime);
                break;
            case 2: // Month
                expireDate = expireDate.AddMonths(validTime);
                break;
            case 3: // Year
                expireDate = expireDate.AddYears(validTime);
                break;
        }

        var applicationToken = _applicationService.CreateApplicationToken(applicationId, expireDate);

        return new ApplicationCreateTokenEntity()
        {
            token = applicationToken,
            ExpireDateTime = expireDate
        };
    }
}