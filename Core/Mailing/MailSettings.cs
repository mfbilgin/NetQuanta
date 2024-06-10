namespace Core.Mailing;

public sealed class MailSettings
{
    public string SmtpServer { get; set; } = "[SMTP_SERVER]";
    public int SmtpPort { get; set; } = 587;
    public string SmtpUserName { get; set; } = "[USERNAME]";
    public string SmtpPassword { get; set; } = "[PASSWORD]";
    public string DisplayName { get; set; } = "[DISPLAY_NAME]";
    public string From { get; set; } = "[FROM_EMAIL]";
}