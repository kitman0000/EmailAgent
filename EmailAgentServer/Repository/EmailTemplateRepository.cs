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
        _dbContext.EmailTemplate.Add(emailTemplate);
        _dbContext.SaveChanges();
    }

    public void InsertSmtp(Smtp smtp)
    {
        _dbContext.Smtp.Add(smtp);
        _dbContext.SaveChanges();
    }
}