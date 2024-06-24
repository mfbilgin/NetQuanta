using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Core.Mailing;

public sealed class MailKitMailManager : IMailService
{
    private const string SiteName = "MFBILGIN BLOG";
    private const string SupportMail = "support@mfbilgin.com.tr";

    private void SendMail(string toEmail, string toUsername, string subject, TextPart body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(MailSettings.DisplayName, MailSettings.From));
        message.To.Add(new MailboxAddress(toUsername, toEmail));
        message.Subject = subject;
        message.Body = body;
        using var smtpClient = new SmtpClient();
        smtpClient.Connect(MailSettings.SmtpServer, MailSettings.SmtpPort, SecureSocketOptions.StartTls);
        smtpClient.Authenticate(MailSettings.SmtpUserName, MailSettings.SmtpPassword);
        smtpClient.Send(message);
        smtpClient.Disconnect(true);
    }

    public void SendWelcomeMail(string email, string username, string token)
    {
        var body = Mails.WelcomeMail;
        body = body.Replace("[Kullanıcı Adı]", username);
        //Doğrulama bağlantısının domain kısmını da kendi sitenize göre düzenleyin.
        body = body.Replace("[Doğrulama Bağlantısı]",
            "https://localhost:7293/api/Auth/verify-email?username=" + username + "&token=" + token);

        //Kesme işaretinden sonraki e-a ekini düzenlemeyi unutmayın.
        body = body.Replace("[Site Adınız]", SiteName);
        body = body.Replace("[Web sitesi destek maili]", SupportMail);
        
        SendMail(email, username, "Aramıza Hoşgeldin", new TextPart("html") { Text = body });
    }

    public void SendPasswordResetMail(string email, string username, string token)
    {
        var body = Mails.PasswordResetMail;
        body = body.Replace("[Kullanıcı Adı]", username);
        body = body.Replace("[Sıfırlama Kodu]", token);

        //Kesme işaretinden sonraki e-a ekini düzenlemeyi unutmayın.
        body = body.Replace("[Site Adınız]", SiteName);
        body = body.Replace("[Web sitesi destek maili]", SupportMail);

        SendMail(email, username, "Şifre Sıfırlama", new TextPart("html") { Text = body });
    }

    public void SendPasswordChangedMail(string email, string username)
    {
        var body = Mails.PasswordChangedMail;
        body = body.Replace("[Kullanıcı Adı]", username);
        
        //Kesme işaretinden sonraki e-a ekini düzenlemeyi unutmayın.
        body = body.Replace("[Site Adınız]", SiteName);
        body = body.Replace("[Web sitesi destek maili]", SupportMail);

        SendMail(email, username, "Şifren Değiştirildi", new TextPart("html") { Text = body });
    }

    public void SendUsernameReminderMail(string email, string username)
    {
        var body = Mails.UsernameReminderMail;
        body = body.Replace("[Kullanıcı Adı]", username);
        
        //Kesme işaretinden sonraki e-a ekini düzenlemeyi unutmayın.
        body = body.Replace("[Site Adınız]", SiteName);
        body = body.Replace("[Web sitesi destek maili]", SupportMail);

        SendMail(email, username, "Kullanıcı Adın Burada", new TextPart("html") { Text = body });
    }
}