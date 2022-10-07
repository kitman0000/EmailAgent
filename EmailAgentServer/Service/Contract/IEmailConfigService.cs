using EmailAgentServer.ApiResponse;

namespace EmailAgentServer.Service.Contract;

public interface IEmailConfigService
{
    AddEmailTemplateResponse AddEmailTemplate(string templateName, IFormFile templateFile, string[] placeHolders);
}