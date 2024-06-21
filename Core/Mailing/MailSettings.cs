namespace Core.Mailing;

public static class MailSettings
{
    public static string SmtpServer => "[SMTP_SERVER]";
    public static int SmtpPort => 587;
    public static string SmtpUserName => "[USERNAME]";
    public static string SmtpPassword => "[PASSWORD]";
    public static string DisplayName => "[DISPLAY_NAME]";
    public static string From => "[FROM_EMAIL]";
}