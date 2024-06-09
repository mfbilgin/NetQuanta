using MimeKit;

namespace Core.Mailing;

public interface IMailService
{
    protected void SendMail(string toEmail, string toUsername, string subject, TextPart body);
    public void SendWelcomeMail(string email,string username,string token);
}