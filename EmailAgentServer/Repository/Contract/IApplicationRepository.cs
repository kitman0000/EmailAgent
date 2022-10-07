using EmailAgentServer.DBEntity;

namespace EmailAgentServer.Repository.Contract;

public interface IApplicationRepository
{
    bool InsertApplication(string applicationName);

    IEnumerable<Application> QueryApplications();

    Application? QueryApplicationById(int id);
}