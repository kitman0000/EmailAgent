using EmailAgentServer.Common;
using EmailAgentServer.DBEntity;
using EmailAgentServer.Entity;
using EmailAgentServer.Repository;
using EmailAgentServer.Repository.Contract;
using EmailAgentServer.Service.Contract;

namespace EmailAgentServer.Service;

public class ApplicationService:IApplicationService
{
    private IApplicationRepository _applicationRepository;
    private SettingHelper _settingHelper;

    public ApplicationService(IApplicationRepository applicationRepository, SettingHelper settingHelper)
    {
        _applicationRepository = applicationRepository;
        _settingHelper = settingHelper;
    }

    public bool CreateApplication(string applicationName)
    {
       return _applicationRepository.InsertApplication(applicationName);
    }

    public IEnumerable<Application> GetApplicationList()
    {
        return _applicationRepository.QueryApplications();
    }

    public string CreateApplicationToken(int applicationId, DateTime expireDate)
    {
        var application = _applicationRepository.QueryApplicationById(applicationId);
        if (application == null)
        {
            return string.Empty;
        }
        
        var key = _settingHelper.GetSetting("ApplicationTokenKey");
        var applicationTokenObject = new ApplicationTokenEntity()
        {
            applicationId = application.Id,
            applicationName = application.applicationName,
            expireDate = expireDate
        };

        var applicationTokenJson = applicationTokenObject.ToJson();
        var applicationToken = EncryptHelper.AesEncryptString(applicationTokenJson, key);
        
        return applicationToken;
    }
}