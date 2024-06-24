namespace Business.Constants.Messages;

public static class UserMessages
{
    // Validation Messages
    public const string UsernameCannotBeEmpty = "Kullanıcı adı boş olamaz.";
    public const string UsernameMustBeAtLeast3Characters = "Kullanıcı adı en az 3 karakter olmalı.";
    public const string UsernameMustBeAtMost50Characters = "Kullanıcı adı en fazla 50 karakter olmalı.";

    public const string FirstNameCannotBeEmpty = "İsim boş olamaz.";
    public const string FirstNameMustBeAtLeast3Characters = "İsim en az 3 karakter olmalı.";
    public const string FirstNameMustBeAtMost100Characters = "İsim en fazla 100 karakter olmalı.";

    public const string LastNameCannotBeEmpty = "Soy isim boş olamaz.";
    public const string LastNameMustBeAtLeast3Characters = "Soy isim en az 3 karakter olmalı.";
    public const string LastNameMustBeAtMost100Characters = "Soy isim en fazla 100 karakter olmalı.";
    
    public const string EmailCannotBeEmpty = "E-posta adresi boş olamaz.";
    public const string EmailMustBeValid = "Geçerli bir e-posta adresi giriniz.";
    
    
    // User Messages
    public const string UsernameAlreadyExists =
        "Bu kullanıcı ismi daha önce alınmış. Lütfen başka bir kullanıcı ismi seçmeyi dene.";

    public const string EmailAlreadyExists = "Bu e-posta adresi daha önce alınmış.";
    public const string UserNotFound = "Kullanıcı bulunamadı lütfen kullanıcı adını kontrol et.";
    public const string CurrentPasswordIsIncorrect = "Parolan yanlış. Lütfen tekrar dene.";
    public const string NewPasswordMustBeDifferent = "Yeni parolan eski parolandan farklı olmalı";
    public const string UserInfosHasBeenUpdated = "Bilgilerin başarıyla güncellendi.";
    public const string UserHasBeenDeleted = "Aramızdan ayrılman üzücü. Hesabın başarıyla silindi.";
    public const string PasswordHasBeenChanged = "Parolan başarıyla değiştirildi. Yeni parolanla giriş yapabilirsin.";
    public const string RoleHasBeenChanged = "Kullanıcı yetkisi değiştirildi.";

    public const string EmailNotVerified =
        "E-posta adresin doğrulanmamış. Lütfen e-posta adresini doğrulamak için e-postana gönderilen linke tıkla.";

    public const string UserRegistered =
        "Aramıza hoş geldin! Hesabın başarıyla oluşturuldu. Posta kutunu kontrol etmeyi unutma.";
    
    public const string EmailAlreadyVerified = "Mail adresin zaten doğrulanmış.";
    public const string UsernameHasBeenSentToEmail = "Kullanıcı adın e-posta adresine gönderildi.";
}