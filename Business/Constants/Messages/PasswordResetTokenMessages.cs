namespace Business.Constants.Messages;

public static class PasswordResetTokenMessages
{
    public const string PasswordResetTokenIsIncorrect = "Sıfırlama kodu yanlış. Lütfen tekrar deneyin.";
    public const string PasswordResetTokenCanNotBeResentInLessThanTwoMinute = "Şifre sıfırlama isteği iki dakikadan daha kısa süre önce gönderilmiş. Lütfen bekleyin.";
    public const string PasswordResetTokenIsNotFound = "Şifre sıfırlaman için bir kod bulunamadı. Lütfen tekrar deneyin.";
    public const string PasswordResetTokenHasBeenSent = "Şifreni sıfırlaman için bir e-posta gönderdik. Lütfen posta kutunu kontrol et.";
    public const string PasswordHasBeenReset = "Şifren başarıyla sıfırlandı. Yeni şifrenle giriş yapabilirsin.";
}