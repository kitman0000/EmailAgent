using EmailAgentServer.ApiResponse;

namespace EmailAgentServer.Service.Contract;

public interface IEmailConfigService
{
    AddEmailTemplateResponse AddEmailTemplate(string templateName,string subject, IFormFile templateFile, string[] placeHolders);
}