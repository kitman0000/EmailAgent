using System.Net;
using System.Net.Mail;

namespace ServerUnitTest;

public class SMTPTest
{
    [Test]
    public void SendSimpleEmail()
    {
        var smtpClient = new SmtpClient("smtp.clanarc.com")
        {
            Port = 25,
            Credentials = new NetworkCredential("develop@clanarc.com", MyPassword.TestEmailPassword),
            EnableSsl = false,
        };
    
        smtpClient.Send("develop@clanarc.com", "ken000666@outlook.com", "Hello, Welcome to ARC Club ", "Hello, Welcome to ARC Club , we provide you a lot of new features! Hope to play with you");
    }
}