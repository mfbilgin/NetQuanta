namespace Business.Constants.Messages;

public static class RoleMessages
{
    public const string RoleNameAlreadyExists = "Bu isimde bir rol zaten var.";
    public const string RoleNotFound = "Rol bulunamadı.";
    public const string RoleHasBeenAdded = "Rol başarıyla eklendi.";
    public const string RoleHasBeenUpdated = "Rol ismi değiştirildi.";
    public const string RoleHasBeenDeleted = "Rol yok edildi. :)";
    
    
    // Validation Messages
    public const string RoleNameCanNotBeEmpty = "Rol ismi boş olamaz.";
    public const string RoleNameCanNotBeNull = "Rol ismi null olamaz.";
    public const string RoleNameMustBeAtLeast3Characters = "Rol ismi en az 3 karakter olmalıdır.";
    public const string RoleNameMustBeAtMost50Characters = "Rol ismi en fazla 50 karakter olmalıdır.";
}