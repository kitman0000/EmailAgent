using EmailAgentServer.DBEntity;

namespace EmailAgentServer.Repository.Contract;

public interface ISettingRepository
{
    void InsertSetting(string key, string value);
    
    void UpdateSetting(string key, string value);

    void RemoveSetting(string key, string value);

    Setting? QuerySetting(string key);
}