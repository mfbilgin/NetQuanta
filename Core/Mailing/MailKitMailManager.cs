using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Core.Mailing;

public sealed class MailKitMailManager : IMailService
{
    public void SendMail(string toEmail, string toUsername, string subject, TextPart body)
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

    public void SendWelcomeMail(string email, string username,string token)
    {
        const string subject = "Aramıza Hoşgeldin";
        var body = @"
<!DOCTYPE html>
<html lang=""tr"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Hoş Geldiniz</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f9f9f9;
            color: #333333;
            margin: 0;
            padding: 0;
        }
        .container {
            width: 80%;
            max-width: 600px;
            margin: 20px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }
        .header {
            text-align: center;
            padding-bottom: 20px;
        }
        .header h1 {
            margin: 0;
            font-size: 24px;
            color: #54443c;
        }
        .content {
            line-height: 1.6;
        }
        .button {
            display: inline-block;
            margin-top: 20px;
            padding: 10px 20px;
            color: #f6f0e9;
            background-color: #54443c;
            text-decoration: none;
            border-radius: 5px;
        }
        .footer {
            margin-top: 20px;
            text-align: center;
            font-size: 12px;
            color: #aaaaaa;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>[Site Adınız]'a Hoş Geldiniz!</h1>
        </div>
        <div class=""content"">
            <p>Merhaba <strong>[Kullanıcı Adı]</strong>,</p>
            <p>[Site Adınız]'a hoş geldin! Seni aramızda görmekten büyük mutluluk duyuyoruz.</p>
            <p>Hesabının başarıyla oluşturulduğunu bildirmek istedik. Ancak, hesabını aktif hale getirmek için e-posta adresini doğrulaman gerekmektedir.</p>
            <p>Hesabını doğrulamak için lütfen aşağıdaki bağlantıya tıkla:</p>
            <p style=""text-align: center;"">
                <a href=""[Doğrulama Bağlantısı]"" target=""_blank"" class=""button"">Hesabınızı Doğrulayın</a>
            </p>
            <p>Eğer bu e-postayı siz talep etmediyseniz, lütfen bu mesajı dikkate almayınız. Güvenliğiniz için hiçbir işlem yapmanız gerekmez.</p>
            <p>Herhangi bir sorun olursa veya yardıma ihtiyacın olursa, bizimle iletişime geçmekten çekinme. Sana yardımcı olmaktan memnuniyet duyarız.</p>
            <p><b>[Site Adınız] Ekibi</b></p>
        </div>
        <div class=""footer"">
            <p>İletişim: <a style=""color:#54443c;"" href=""mailto:[Web sitesi destek maili]"">[Web sitesi destek maili]</a></p>
        </div>
    </div>
</body>
</html>
";
        body = body.Replace("[Kullanıcı Adı]", username);
        //Doğrulama bağlantısının domain kısmını da kendi sitenize göre düzenleyin.
        body = body.Replace("[Doğrulama Bağlantısı]",
            "https://localhost:7293/api/Auth/verify-email?username=" + username + "&token=" + token);

        //Kesme işaretinden sonraki e-a ekini düzenlemeyi unutmayın.
        body = body.Replace("[Site Adınız]", "Sitenizin adı buraya");
        body = body.Replace("[Web sitesi destek maili]", "support@sitenizinadı.com");


        SendMail(email, username, subject, new TextPart("html") { Text = body });
    }
}