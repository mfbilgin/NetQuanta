namespace Business.Constants.Messages;

public static class EmailVerificationMessages
{
    public const string EmailVerificationTokenIsIncorrect =
        "Bu link doğru görünmüyor. Posta kutunu kontrol edebilir veya yeni bir doğrulama postası isteği gönderebilirsin.";

    public const string EmailHasBeenVerified = "E-posta adresin doğrulandı. Artık giriş yapabilirsin.";
    public const string EmailHasBeenSent = "E-posta adresine doğrulama postası gönderildi. Lütfen posta kutunu kontrol et.";
    public const string EmailVerificationTokenCanNotBeResentInLessThanTwoMinute = "Doğrulama postası en az iki dakika aralıklarla gönderilebilir. Lütfen bekleyin.";
}