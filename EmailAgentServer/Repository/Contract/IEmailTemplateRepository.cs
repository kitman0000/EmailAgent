using EmailAgentServer.DBEntity;

namespace EmailAgentServer.Repository.Contract;

public interface IEmailTemplateRepository
{
    void InsertEmailTemplate(EmailTemplate emailTemplate);
}