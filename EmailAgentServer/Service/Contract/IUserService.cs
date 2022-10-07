namespace EmailAgentServer.Service.Contract;

public interface IUserService
{
    void AddUser(string username, string password, bool isRoot);

    /// <summary>
    /// Login a user and create a valid token
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>
    /// Success:Token
    /// Failed:Empty String
    /// </returns>
    string LoginUser(string username, string password);
}