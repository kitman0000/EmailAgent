using EmailAgentServer.ApiResponse;

namespace EmailAgentServer.Service.Contract;

public interface IEmailConfigService
{
    AddEmailTemplateResponse AddEmailTemplate(string templateName,string subject, IFormFile templateFile, string[] placeHolders);

    void AddSmtp(string host, int port, string username, string password, bool enableSsl, int applicationId);
}