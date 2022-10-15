using EmailAgentServer.ApiResponse;
using EmailAgentServer.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmailAgentServer.Controllers;

[Route("/EmailConfig")]
[ApiController]
public class EmailConfigController: ControllerBase
{
    private IEmailConfigService _emailConfigService;

    public EmailConfigController(IEmailConfigService emailConfigService)
    {
        _emailConfigService = emailConfigService;
    }

    [Authorize(Roles = "User")]
    [HttpPost("EmailTemplate")]
    public ActionResult<AddEmailTemplateResponse> AddEmailTemplate(string templateName, string subject, IFormFile templateFile, [FromQuery] string[] parameters)
    {
        return _emailConfigService.AddEmailTemplate(templateName,subject,templateFile,parameters);
    }

    [Authorize(Roles = "User")]
    [HttpPost("Smtp")]
    public IActionResult AddSmtp(string host, int port, string username, string password, bool enableSsl,int applicationId)
    {
        _emailConfigService.AddSmtp(host,port,username,password,enableSsl,applicationId);
        return Ok();
    }
}