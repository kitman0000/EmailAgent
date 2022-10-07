using EmailAgentServer.DBEntity;
using EmailAgentServer.Repository.Contract;

namespace EmailAgentServer.Repository;

public class ApplicationRepository:IApplicationRepository
{
    private EmailAgentDbContext _emailAgentDbContext;

    public ApplicationRepository(EmailAgentDbContext emailAgentDbContext)
    {
        _emailAgentDbContext = emailAgentDbContext;
    }

    public bool InsertApplication(string applicationName)
    {
        var application = new Application()
        {
            applicationName = applicationName
        };

        try
        {
            _emailAgentDbContext.Application.Add(application);
            _emailAgentDbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public IEnumerable<Application> QueryApplications()
    {
        return _emailAgentDbContext.Application.ToList();
    }

    public Application? QueryApplicationById(int id)
    {
        return _emailAgentDbContext.Application.Find(id);
    }
}