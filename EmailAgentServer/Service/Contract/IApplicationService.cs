using EmailAgentServer.DBEntity;

namespace EmailAgentServer.Service.Contract;

public interface IApplicationService
{
    bool CreateApplication(string applicationName);

    IEnumerable<Application> GetApplicationList();

    string CreateApplicationToken(int applicationId, DateTime expireDate);
    
}