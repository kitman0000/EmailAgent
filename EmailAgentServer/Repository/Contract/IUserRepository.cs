using EmailAgentServer.DBEntity;

namespace EmailAgentServer.Repository.Contract;

public interface IUserRepository
{
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="user">User Entity</param>
    /// <returns>
    /// True: Create success
    /// False: Username already exist, abort creating.
    /// </returns>
    bool AddUser(User user);

    User? QueryUser(string username);
}