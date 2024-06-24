namespace Core.Mailing;

public interface IMailService
{
    public void SendWelcomeMail(string email,string username,string token);
    public void SendPasswordResetMail(string email,string username,string token);
    public void SendPasswordChangedMail(string email, string username);
    public void SendUsernameReminderMail(string email, string username);
}