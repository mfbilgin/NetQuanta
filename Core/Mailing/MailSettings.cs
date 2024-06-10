namespace Core.Mailing;

public sealed class MailSettings
{
    public string SmtpServer { get; } = "[SMTP_SERVER]";
    public int SmtpPort { get; } = 587;
    public string SmtpUserName { get; } = "[USERNAME]";
    public string SmtpPassword { get; } = "[PASSWORD]";
    public string DisplayName { get; } = "[DISPLAY_NAME]";
    public string From { get; } = "[FROM_EMAIL]";
}