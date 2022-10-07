using System.Text.RegularExpressions;
using EmailAgentServer.ApiResponse;
using EmailAgentServer.DBEntity;
using EmailAgentServer.Repository.Contract;
using EmailAgentServer.Service.Contract;

namespace EmailAgentServer.Service;

public class EmailConfigService:IEmailConfigService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailTemplateRepository _emailTemplateRepository;

    public EmailConfigService(IConfiguration configuration, IEmailTemplateRepository emailTemplateRepository)
    {
        _configuration = configuration;
        _emailTemplateRepository = emailTemplateRepository;
    }
    
    public AddEmailTemplateResponse AddEmailTemplate(string templateName, IFormFile templateFile, string[] placeHolders)
    {
        var location = _configuration["templateLocation"];
        var path = location + "/" + templateName;
        if (File.Exists(path))
        {
            return new AddEmailTemplateResponse()
            {
                IsSuccess = false,
                FailedReason = "Template Already exist"
            };
        }
        using (var stream = System.IO.File.Create(path))
        {
            templateFile.CopyToAsync(stream);
        }

        var templateContent = File.ReadAllText(path);
        var  missingParameters=  VerifyTemplatePlaceHolders(templateContent, placeHolders);
        var  missingPlaceHolders = VerifyTemplateParameters(templateContent, placeHolders);
        var isSuccess = missingParameters.Length == 0 && missingPlaceHolders.Length == 0;
        
        AddEmailTemplateResponse response;
        
        if (isSuccess)
        {
            SaveTemplate(templateName,path,templateContent,placeHolders);

             response = new AddEmailTemplateResponse()
            {
                IsSuccess = true,
                MissingPlaceHolder = null,
                MissingParameters = null
            };
        }
        else
        {
            response = new AddEmailTemplateResponse()
            {
                IsSuccess = false,
                MissingPlaceHolder = missingPlaceHolders,
                MissingParameters = missingParameters,
                FailedReason = "There are missing values, refer to details"
            };
            File.Delete(path);
        }
        
        return response;
    }
    
    /// <summary>
    /// Check all the placeholders in the file are in the parameters
    /// </summary>
    /// <param name="template"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private string[] VerifyTemplatePlaceHolders(string template, string[] parameters)
    {
        var missingParameters = new List<string>();
        foreach (Match match in Regex.Matches(template, @"\${.*?}"))
        {
            var value = match.Value.Substring(2, match.Length - 3);
            
            if (!parameters.Contains(value))
            {
                missingParameters.Add(value);
            }
        }

        return missingParameters.ToArray();
    }

    /// <summary>
    /// Check all the parameters are in the file.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    private string[] VerifyTemplateParameters(string template, string[] parameters)
    {
        var missingPlaceHolders = new List<string>();
        foreach (var parameter in parameters)
        {
            if (!template.Contains(parameter))
            {
                missingPlaceHolders.Add(parameter);
            }
        }

        return missingPlaceHolders.ToArray();
    }

    /// <summary>
    /// Save the template to database
    /// </summary>
    /// <param name="templateName"></param>
    /// <param name="templateContent"></param>
    /// <param name="placeHolders"></param>
    private void SaveTemplate(string templateName, string filePath, string templateContent, string[] placeHolders)
    {
        var emailPlaceholderList = new List<EmailPlaceholder>();
        foreach (var placeHolder in placeHolders)
        {
            emailPlaceholderList.Add(new EmailPlaceholder()
            {
                PlaceHolder = placeHolder
            });
        }

        var emailTemplate = new EmailTemplate()
        {
            TemplateName = templateName,
            FilePath = filePath,
            EmailPlaceholders = emailPlaceholderList
        };
        
        _emailTemplateRepository.InsertEmailTemplate(emailTemplate);
    }

}