using EmailAgentServer.Repository;
using EmailAgentServer.Repository.Contract;

namespace EmailAgentServer.Common;

public class SettingHelper
{
    private readonly ISettingRepository _settingRepository;
    
    public SettingHelper(ISettingRepository settingRepository)
    {
        _settingRepository = settingRepository;
    }

    public void AddSetting(string key, string value)
    {
        _settingRepository.InsertSetting(key,value);
    }
    
    public void UpdateSetting(string key, string value)
    {
        _settingRepository.UpdateSetting(key,value);
    }
    
    public void RemoveSetting(string key, string value)
    {
        _settingRepository.RemoveSetting(key,value);
    }

    public string GetSetting(string key)
    {
        var setting = _settingRepository.QuerySetting(key);
        if (setting is null)
        {
            return string.Empty;
        }

        return setting.value;
    }
}