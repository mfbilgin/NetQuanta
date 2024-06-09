namespace Core.Mailing;

public sealed class MailSettings
{
    public string SmtpServer { get; set; } = "smtp.turkticaret.net";
    public int SmtpPort { get; set; } = 587;
    public string SmtpUserName { get; set; } = "admin@mfbilgin.com.tr";
    public string SmtpPassword { get; set; } = "&[\"-f#\"XC5aw5'+)";
    public string DisplayName { get; set; } = "Silversoft";
    public string From { get; set; } = "admin@mfbilgin.com.tr";
}