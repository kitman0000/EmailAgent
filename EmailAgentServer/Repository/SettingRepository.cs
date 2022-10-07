using EmailAgentServer.DBEntity;
using EmailAgentServer.Repository.Contract;

namespace EmailAgentServer.Repository;

public class SettingRepository:ISettingRepository
{
    private EmailAgentDbContext _emailAgentDbContext;
    
    public SettingRepository(EmailAgentDbContext emailAgentDbContext)
    {
        _emailAgentDbContext = emailAgentDbContext;
    }


    public void InsertSetting(string key, string value)
    {
        Setting setting = new Setting();
        setting.key = key;
        setting.value = value;

        _emailAgentDbContext.Add(setting);
        _emailAgentDbContext.SaveChanges();
    }

    public void UpdateSetting(string key, string value)
    {
        var setting = _emailAgentDbContext.Setting.First(s => s.key == key);
        setting.key = key;
        setting.value = value;

        _emailAgentDbContext.SaveChanges();
    }

    public void RemoveSetting(string key, string value)
    {
        var setting = _emailAgentDbContext.Setting.First(s => s.key == key);
        _emailAgentDbContext.Remove(setting);
        _emailAgentDbContext.SaveChanges();
    }

    public Setting? QuerySetting(string key)
    {
        if (!_emailAgentDbContext.Setting.Any())
        {
            return null;
        }
        return _emailAgentDbContext.Setting.FirstOrDefault(s => s.key == key);
    }
}