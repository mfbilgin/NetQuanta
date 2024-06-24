# EN
# NETQUANTA (.NET Core 8.0 Project Template)

This repository provides a ready-to-use template for .NET Core 8.0 projects. It includes essential features and a structured architecture to accelerate your development process.

## Features

- **Layered Architecture**: Separation of concerns with Entities, DataAccess, Business, Core, and WebAPI layers.
- **Entity Framework Core Integration**: Pre-configured for database access and management.
- **Dapper Integration**: Support for those who want faster database access than EF.
- **Repository Pattern**: Abstracts data access logic for better testability and maintainability.
- **Dependency Injection**: Use of dependency injection to manage service lifecycles and dependencies.
- **Swagger**: Integrated Swagger for API documentation and testing.

## Getting Started

To start using the project template, follow these steps:

### Requirements

- [.NET Core SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. **Start by cloning the repository:**

    ```
    git clone https://github.com/mfbilgin/.NetCore8.0-ProjectTemplate.git
    ```
    Or you can download it as a zip file from the code section in the upper right corner.

2. **Restore NuGet packages:**

    ```
    cd .NetCore8.0-ProjectTemplate
    dotnet restore
    ```

### Configuration

1. Configure the connection strings in the `appsettings.json` file.

    ```
    "ConnectionStrings": {  
      "DefaultConnection": "Server=MFBILGIN;Initial Catalog=TemplateDb;User=[YOUR_USERNAME];Password=[YOUR_PASSWORD];TrustServerCertificate=True;"  
    }
    ```
    
    If you are not using username and password for database connection:
    
    ```
    "ConnectionStrings": {  
      "DefaultConnection": "Server=MFBILGIN;Initial Catalog=TemplateDb;Trusted_Connection=True;TrustServerCertificate=True;"  
    }
    ```

2. Update the Token Options information in the `appsettings.json` file according to your needs. Ensure the SecurityKey is more than 64 characters.

   ```
    "TokenOptions": {  
    "Audience": "```",  
    "Issuer": "```",  
    "SecurityKey": "[YOUR_SECURITY_KEY]",  
    "AccessTokenExpiration": "YOUR_ACCESS_TOKEN_EXPIRATION(MINUTES) as int"  
    }
    ```

3. If you are using EF, change the registration of the Dapper context to the EF context in the `DataAccessServiceRegistration.cs` file, then create a migration and update the database.

4. Fill in the `MailSettings.cs` information in Core/Mailing with your own details. Then make the necessary adjustments in the functions within `MailKitMailManager.cs`.

5. The project is now ready to run. First, please remove the admin check in the Add function located in `Business/Concrete/RoleManager.cs` and in the ChangeUserRole function located in `Business/Concrete/UserManager.cs`. Add the roles in `Core/Entities/Enum`, then create an admin account and assign the admin role, and finally re-add the admin checks.

### Using the Template

#### Entities Layer

Define your entities here. For example, create a `Product` entity:

```csharp
public sealed class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

#### Data Access Layer

Define repositories that provide database connections here. For example, create the `EfProductRepository` class:
```csharp
public sealed class EfProductRepository(DbContext context) : EfEntityRepositoryBase<Product>(context), IProductRepository  
{  
    public Product? GetByName(string name)  
    {       
        return Get(product => product.Name.ToLower() == name.ToLower());  
    }
}
```

#### Business Layer

Define your business rules and validations here. Example code is omitted for brevity.

#### WebAPI Layer

This is the entry point of your project to the outside world. Define your *Controller functions* here.
---
That's all. Happy coding!

---
# TR
# NETQUANTA (.NET Core 8.0 Proje Şablonu)

Bu repo, .NET Core 8.0 projeleri için kullanıma hazır bir şablon sunar. Geliştirme sürecinizi hızlandırmak için gerekli temel özellikleri ve yapılandırılmış bir mimariyi içerir.

## Özellikler

- **Katmanlı Mimari**: Entities, DataAccess,Business, Core ve WebAPI katmanları ile sorumlulukların ayrılması.
- **Entity Framework Core Entegrasyonu**: Veritabanı erişimi ve yönetimi için önceden yapılandırılmış.
- **Dapper Entegrasyonu**: Veritabanına ef'ten daha hızlı erişim sağlamak isteyenler için dapper desteği.
- **Repository Pattern**: Veri erişim mantığını soyutlayarak daha iyi test edilebilirlik ve sürdürülebilirlik sağlar.
- **Dependency Injection**: Servis yaşam döngülerini ve bağımlılıkları yönetmek için dependency injection yönteminin kullanılması.
- **Swagger**: API dokümantasyonu ve test için entegre Swagger.

## Başlarken

Proje şablonunu kullanmaya başlamak için aşağıdaki adımları izleyebilirsiniz:

### Gereksinimler

- [.NET Core SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Kurulum

1. **Depoyu klonlamakla başlayalım:**

    ```
    git clone https://github.com/mfbilgin/.NetCore8.0-ProjectTemplate.git
    ```
	Ya da dilereseniz sağ üstteki code sekmesi altından zip olarak da indirebilirsiniz.


2. **NuGet paketlerini geri yükleyin:**

    ```
    cd .NetCore8.0-ProjectTemplate
    dotnet restore
    ```

### Yapılandırma

1. `appsettings.json` dosyasındaki bağlantı stringlerini yapılandırın.

    ```
    "ConnectionStrings": {  
	  "DefaultConnection": "Server=MFBILGIN;Initial Catalog=TemplateDb;User=[YOUR_USERNAME];Password=[YOUR_PASSWORD];TrustServerCertificate=True;"  
    }
    ```
    
    Eğer veri tabanına bağlanırken kullanıcı adı ve parola kullanarak bağlanmıyorsanız:
    
    ```
    "ConnectionStrings": {  
	  "DefaultConnection": "Server=MFBILGIN;Initial Catalog=TemplateDb;Trusted_Connection=True;TrustServerCertificate=True;"  
    }
    ```

2. `appsettings.json` dosyasındaki Token Options bilgilerini kendinize uygun olarak güncelleyin. SecurtiyKey'in 64 karakterden fazla olmasına dikkat edin.

   ```
    "TokenOptions": {  
    "Audience": "```",  
    "Issuer": "```",  
    "SecurityKey": "[YOUR_SECURITY_KEY]",  
    "AccessTokenExpiration": "YOUR_ACCESS_TOKEN_EXPIRATION(MINUTES) as int"  
    }
    ```

3. Eğer EF kullanacaksanız `DataAccessServiceRegistration.cs` dosyasında dapper context'in kaydını ef context olarak değiştirin ve ardından migration oluşturup veri tabanını güncelleyin. 

4. Core/Mailing'de yer alan `MailSettings.cs` bilgilerini kendi bilgilerinize göre doldurun. Ve ardından `MailKitMailManager.cs` içerisindeki fonksiyonlarda yer alan düzenlemeleri gerçekleştirin.

5. Proje çalıştırılmaya hazır. İlk olarak lütfen ```Business/Concrete/RoleManager.cs```'de yer alan Add fonksiyonunun ve ```Business/Concrete/UserManager.cs```'de yer alan ChangeUserRole fonksiyonunun admin kontrolünün kaldırıp ```Core/Entities/Enum```'da yer alan rolleri ekleyin daha sonra bir admin hesabı açıp admin rolü verin ve daha sonra admin kontrolerini yeniden ekleyin

### Şablonun Kullanımı

#### Entites Katmanı

Varlıklarınızı burada tanımlayın. Örneğin, bir `Product` varlığı oluşturun:

```csharp
public sealed class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

#### Data Access Katmanı

Veri tabanı bağlantılarını sağlayan repositoryleri burada tanımlayın. Örneğin `EfProductRepository` sınıfını oluşturun
```csharp
public sealed class EfProductRepository(DbContext context) : EfEntityRepositoryBase<Product>(context),IProductRepository  
{  
    public Product? GetByName(string name)  
    {       
        return Get(product => product.Name.ToLower() == name.ToLower());  
    }
}
```

#### Business Katmanı

İş kurallarınızı ve validasyonlarınızı burada tanımlayın. Örnek kod biraz uzun olacağı için es geçiyorum.


#### WebAPI Katmanı

Projenizin dış dünyaya açılan kapısı burası. Fonksiyonlarınız için *Controller fonksiyonlarını* burada tanımlayın.




# İşte hepsi bu kadar. İyi çalışmalar.
