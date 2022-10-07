using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmailAgentServer.Common;
using EmailAgentServer.DBEntity;
using EmailAgentServer.Repository;
using EmailAgentServer.Repository.Contract;
using EmailAgentServer.Service.Contract;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace EmailAgentServer.Service;

public class UserService:IUserService
{
    private IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(string username, string password, bool isRoot)
    {
        var user = new User()
        {
            UserName = username,
            Password = EncryptHelper.MD5Hash(password,ConstantValue.UserPasswordMd5Key),
            IsRoot = isRoot
        };
        _userRepository.AddUser(user);
    }

    public string LoginUser(string username, string password)
    {
        var user =_userRepository.QueryUser(username);

        password = EncryptHelper.MD5Hash(password, ConstantValue.UserPasswordMd5Key);
        if (user == null || user.Password != password)
        {
            return string.Empty;
        }
        
        var key = Encoding.ASCII.GetBytes(ConstantValue.TokenGenerateKey);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(JwtClaimTypes.Id, user.Id.ToString()),
                new(JwtClaimTypes.Name, username),
                new(JwtClaimTypes.Role,"User"),
                new("isRoot",user.IsRoot.ToString())
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
        
    }
}