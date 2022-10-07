using EmailAgentServer.DBEntity;
using EmailAgentServer.Repository.Contract;

namespace EmailAgentServer.Repository;

public class UserRepository:IUserRepository
{
    private EmailAgentDbContext _emailAgentDbContext;

    public UserRepository(EmailAgentDbContext emailAgentDbContext)
    {
        _emailAgentDbContext = emailAgentDbContext;
    }
    
    public bool AddUser(User user)
    {
        if (QueryUser(user.UserName) != null)
        {
            return false;
        }
        
        _emailAgentDbContext.Add(user);
        _emailAgentDbContext.SaveChanges();

        return true;
    }

    public User? QueryUser(string username)
    {
        if (!_emailAgentDbContext.User.Any())
        {
            return null;
        }
        return _emailAgentDbContext.User.FirstOrDefault(u => (u.UserName == username));
    }
}