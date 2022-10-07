using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmailAgentServer.Common;
using EmailAgentServer.Entity;
using EmailAgentServer.Repository;
using EmailAgentServer.Service.Contract;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmailAgentServer.Controllers;

[Route("/Admin")]
[ApiController]
public class AdminController : ControllerBase
{
    private SettingHelper _settingHelper;
    private IUserService _userService;
    private IApplicationService _applicationService;

    public AdminController(SettingHelper settingHelper, IUserService userService, IApplicationService applicationService)
    {
        _settingHelper = settingHelper;
        _userService = userService;
        _applicationService = applicationService;
    }
    
    [HttpPost("InitializeServer")]
    public IActionResult InitializeServer(string rootPassword)
    {
        if (_settingHelper.GetSetting("isInitialized") == "True")
        {
            return Ok(new
            {
                result = "NOT_AVAILABLE"
            });
        }

        if (_settingHelper.GetSetting("isInitialized") != string.Empty)
        {
            return Ok(new
            {
                result = "SYSTEM_ERROR"
            });
        }
        
        _settingHelper.AddSetting("isInitialized","False");

        #region Start of the initial progress
        _userService.AddUser("root",rootPassword,true);
        _applicationService.CreateApplication("DemoApplication");
        _settingHelper.AddSetting("ApplicationTokenKey", EncryptHelper.CreateRandomKey(32));
        #endregion
        _settingHelper.UpdateSetting("isInitialized","True");
        return Ok(new
        {
            result = "SUCCESS"
        });
        

    }
    
    [HttpPost( "Authenticate")]
    public ActionResult<AdminAuthenticateEntity> Authenticate(string username, string password)
    {
        var token = _userService.LoginUser(username, password);

        return new AdminAuthenticateEntity
        {
            IsSuccess = (token != string.Empty),
            AccessToken = token
        };
    }

    [Authorize(Roles = "User")]
    [HttpGet("AuthorizeTest")]
    public string Test()
    {
        return "Test ok!";
    }

    // [Authorize(Roles = "Admin")]
    [HttpGet("videos")]
    public String GetVideos()
    {
        return DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
    }

  
}