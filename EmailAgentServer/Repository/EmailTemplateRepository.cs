using EmailAgentServer.DBEntity;
using EmailAgentServer.Repository.Contract;

namespace EmailAgentServer.Repository;

public class EmailTemplateRepository: IEmailTemplateRepository
{
    private EmailAgentDbContext _dbContext;


    public EmailTemplateRepository(EmailAgentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void InsertEmailTemplate(EmailTemplate emailTemplate)
    {
        _dbContext.EmailTemplates.Add(emailTemplate);
        _dbContext.SaveChanges();
    }
}