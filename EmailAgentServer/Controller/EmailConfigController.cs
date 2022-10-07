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
    public ActionResult<AddEmailTemplateResponse> AddEmailTemplate(string templateName, IFormFile templateFile, [FromQuery] string[] parameters)
    {
        return _emailConfigService.AddEmailTemplate(templateName, templateFile,parameters);
    }
}