# .NET Core 8.0 Proje Şablonu

Bu depo, .NET Core 8.0 projeleri için kullanıma hazır bir şablon sunar. Geliştirme sürecinizi hızlandırmak için gerekli temel özellikleri ve yapılandırılmış bir mimariyi içerir.

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
    "AccessTokenExpiration": "YOUR_ACCESS_TOKEN_EXPIRATION(MINUTES)"  
    }
    ```

3. Eğer EF kullanacaksanız `DataAccessServiceRegistration.cs` dosyasında dapper context'in kaydını ef context olarak değiştirin ve ardından migration oluşturup veri tabanını güncelleyin. 

4. Core/Mailing'de yer alan `MailSettings.cs` bilgilerini kendi bilgilerinize göre doldurun. Ve ardından `MailKitMailManager.cs` içerisinde yer alan send welcome mail fonksiyonunda yer alan düzenlemeleri gerçekleştirin.

5. Proje çalıştırılmaya hazır. İlk olarak lütfen admin kontrolünü kaldırıp rol ekleyin ve öyle devam edin :)

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
