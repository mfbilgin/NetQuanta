namespace Core.Mailing;

public static class Mails
{
    public const string WelcomeMail = """
                                      <!DOCTYPE html>
                                      <html lang="tr">
                                      <head>
                                          <meta charset="UTF-8">
                                          <meta name="viewport" content="width=device-width, initial-scale=1.0">
                                          <style>
                                              body {
                                                  font-family: Arial, sans-serif;
                                                  background-color: #f9f9f9;
                                                  color: #333333;
                                                  margin: 0;
                                                  padding: 0;
                                              }
                                              .container {
                                                  width: 80%;
                                                  max-width: 600px;
                                                  margin: 20px auto;
                                                  background-color: #ffffff;
                                                  padding: 20px;
                                                  border-radius: 10px;
                                                  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                              }
                                              .header {
                                                  text-align: center;
                                                  padding-bottom: 20px;
                                              }
                                              .header h1 {
                                                  margin: 0;
                                                  font-size: 24px;
                                                  color: #54443c;
                                              }
                                              .content {
                                                  line-height: 1.6;
                                              }
                                              .button {
                                                  display: inline-block;
                                                  margin-top: 20px;
                                                  padding: 10px 20px;
                                                  color: #f6f0e9;
                                                  background-color: #54443c;
                                                  text-decoration: none;
                                                  border-radius: 5px;
                                              }
                                              .footer {
                                                  margin-top: 20px;
                                                  text-align: center;
                                                  font-size: 12px;
                                                  color: #aaaaaa;
                                              }
                                          </style>
                                      </head>
                                      <body>
                                          <div class="container">
                                              <div class="header">
                                                  <h1>[Site Adınız]'a Hoş Geldiniz!</h1>
                                              </div>
                                              <div class="content">
                                                  <p>Merhaba <strong>[Kullanıcı Adı]</strong>,</p>
                                                  <p>[Site Adınız]'a hoş geldin! Seni aramızda görmekten büyük mutluluk duyuyoruz.</p>
                                                  <p>Hesabının başarıyla oluşturulduğunu bildirmek istedik. Ancak, hesabını aktif hale getirmek için e-posta adresini doğrulaman gerekmektedir.</p>
                                                  <p>Hesabını doğrulamak için lütfen aşağıdaki bağlantıya tıkla:</p>
                                                  <p style="text-align: center;">
                                                      <a href="[Doğrulama Bağlantısı]" target="_blank" class="button">Hesabınızı Doğrulayın</a>
                                                  </p>
                                                  <p>Eğer bu e-postayı siz talep etmediyseniz, lütfen bu mesajı dikkate almayınız. Güvenliğiniz için hiçbir işlem yapmanız gerekmez.</p>
                                                  <p>Herhangi bir sorun olursa veya yardıma ihtiyacın olursa, bizimle iletişime geçmekten çekinme. Sana yardımcı olmaktan memnuniyet duyarız.</p>
                                                  <p><b>[Site Adınız] Ekibi</b></p>
                                              </div>
                                              <div class="footer">
                                                  <p>İletişim: <a style="color:#54443c;" href="mailto:[Web sitesi destek maili]">[Web sitesi destek maili]</a></p>
                                              </div>
                                          </div>
                                      </body>
                                      </html>
                                      """;

    public const string PasswordResetMail = """
                                            <!DOCTYPE html>
                                            <html lang='tr'>
                                            <head>
                                                <meta charset='UTF-8'>
                                                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                                                <style>
                                                    body {
                                                        font-family: Arial, sans-serif;
                                                        line-height: 1.6;
                                                        color: #333;
                                                        background-color: #f4f4f4;
                                                        margin: 0;
                                                        padding: 0;
                                                    }
                                                    .container {
                                                        max-width: 600px;
                                                        margin: 20px auto;
                                                        background-color: #fff;
                                                        padding: 20px;
                                                        border-radius: 8px;
                                                        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                                    }
                                                    .header {
                                                        text-align: center;
                                                        padding-bottom: 20px;
                                                    }
                                                    .header h1 {
                                                        margin: 0;
                                                        color: #54443c;
                                                    }
                                                    .content {
                                                        margin: 20px 0;
                                                    }
                                                    .button {
                                                        display: inline-block;
                                                        padding: 10px 20px;
                                                        color: #f6f0e9;
                                                        background-color: #54443c;
                                                        text-decoration: none;
                                                        border-radius: 5px;
                                                    }
                                                    .footer {
                                                        text-align: center;
                                                        font-size: 12px;
                                                        color: #777;
                                                        margin-top: 20px;
                                                    }
                                                </style>
                                            </head>
                                            <body>
                                                <div class='container'>
                                                    <div class='header'>
                                                        <h1>Şifre Sıfırlama</h1>
                                                    </div>
                                                    <div class='content'>
                                                        <p>Merhaba [Kullanıcı Adı],</p>
                                                        <p>Hesabın için bir şifre sıfırlama isteği aldık. Şifreni sıfırlamak için aşağıdaki kodu kullanabilirsin</p>
                                                        <p style='text-align: center;'>
                                                            <strong>[Sıfırlama Kodu]</strong>
                                                        </p>
                                                        </p>
                                                        <p>Eğer bu isteği sen yapmadıysan, lütfen bu e-postayı dikkate alma.</p>
                                                        <p>Teşekkürler,<br>[Site Adınız] Destek Ekibi</p>
                                                    </div>
                                                    <div class='footer'>
                                                        <p>Bu mesaj otomatik olarak oluşturulmuştur. Lütfen yanıtlamayın.</p>
                                                        <p>İletişim: <a style="color:#54443c;" href="mailto:[Web sitesi destek maili]">[Web sitesi destek maili]</a></p>
                                                    </div>
                                                </div>
                                            </body>
                                            </html>
                                            """;

    public const string PasswordChangedMail = """
                                              <!DOCTYPE html>
                                              <html lang="tr">
                                              <head>
                                                  <meta charset="UTF-8">
                                                  <meta name="viewport" content="width=device-width, initial-scale=1.0">
                                                  <style>
                                                      body {
                                                          font-family: Arial, sans-serif;
                                                          background-color: #f9f9f9;
                                                          color: #333333;
                                                          margin: 0;
                                                          padding: 0;
                                                      }
                                                      .container {
                                                          width: 80%;
                                                          max-width: 600px;
                                                          margin: 20px auto;
                                                          background-color: #ffffff;
                                                          padding: 20px;
                                                          border-radius: 10px;
                                                          box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                                      }
                                                      .header {
                                                          text-align: center;
                                                          padding-bottom: 20px;
                                                      }
                                                      .header h1 {
                                                          margin: 0;
                                                          font-size: 24px;
                                                          color: #54443c;
                                                      }
                                                      .content {
                                                          line-height: 1.6;
                                                      }
                                                      .button {
                                                          display: inline-block;
                                                          margin-top: 20px;
                                                          padding: 10px 20px;
                                                          color: #f6f0e9;
                                                          background-color: #54443c;
                                                          text-decoration: none;
                                                          border-radius: 5px;
                                                      }
                                                      .footer {
                                                          margin-top: 20px;
                                                          text-align: center;
                                                          font-size: 12px;
                                                          color: #aaaaaa;
                                                      }
                                              		
                                              		.footer p a{
                                              			text-decoration: none;
                                              		}
                                              		.footer p a:hover{
                                              			text-decoration: underline;
                                              		}
                                              		
                                              		.content p{
                                                        text-align: center;
                                                     }
                                                  </style>
                                              </head>
                                              <body>
                                                  <div class="container">
                                                      <div class="header">
                                                          <h1>Parolan Değiştirildi!</h1>
                                                      </div>
                                                      <div class="content">
                                                          <p>Merhaba <strong>[Kullanıcı Adı]</strong>,</p>    
                                                          <p>Az önce parolan değiştirildi.</p>
                                                          <p>Eğer bu işlem bilgin dışında gerçekleştiyse lütfen bize bildir</p>
                                                          <p><b>[Site Adınız] Ekibi</b></p>
                                                      </div>
                                                      <div class="footer">
                                              		
                                                          <p>İletişim: <a style="color:#54443c;" href="mailto:[Web sitesi destek maili]">[Web sitesi destek maili]</a></p>
                                              			<p>Bu mesaj otomatik olarak oluşturulmuştur. Lütfen yanıtlamayın.</p>
                                                      </div>
                                                  </div>
                                              </body>
                                              </html>
                                              """;

    public const string UsernameReminderMail = """
                                               <!DOCTYPE html>
                                               <html lang="tr">
                                               <head>
                                                   <meta charset="UTF-8">
                                                   <meta name="viewport" content="width=device-width, initial-scale=1.0">
                                                   <style>
                                                       body {
                                                           font-family: Arial, sans-serif;
                                                           background-color: #f9f9f9;
                                                           color: #333333;
                                                           margin: 0;
                                                           padding: 0;
                                                       }
                                                       .container {
                                                           width: 80%;
                                                           max-width: 600px;
                                                           margin: 20px auto;
                                                           background-color: #ffffff;
                                                           padding: 20px;
                                                           border-radius: 10px;
                                                           box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                                       }
                                                       .header {
                                                           text-align: center;
                                                           padding-bottom: 20px;
                                                       }
                                                       .header h1 {
                                                           margin: 0;
                                                           font-size: 24px;
                                                           color: #54443c;
                                                       }
                                                       .content {
                                                           line-height: 1.6;
                                                       }
                                                       .button {
                                                           display: inline-block;
                                                           margin-top: 20px;
                                                           padding: 10px 20px;
                                                           color: #f6f0e9;
                                                           background-color: #54443c;
                                                           text-decoration: none;
                                                           border-radius: 5px;
                                                       }
                                                       .footer {
                                                           margin-top: 20px;
                                                           text-align: center;
                                                           font-size: 12px;
                                                           color: #aaaaaa;
                                                       }
                                               		
                                               		.footer p a{
                                               			text-decoration: none;
                                               		}
                                               		.footer p a:hover{
                                               			text-decoration: underline;
                                               		}
                                               		.content p{
                                               		    text-align: center;
                                               		}
                                                   </style>
                                               </head>
                                               <body>
                                                   <div class="container">
                                                       <div class="header">
                                                           <h1>Kullanıcı Adın Burada</h1>
                                                       </div>
                                                       <div class="content">
                                                           <p>Merhaba az önce kullanıcı adını unuttuğunu duyduk işte kullanıcı adın,</p>    
                                                           <p>
                                                   <strong>[Kullanıcı Adı]</strong>
                                               </p>
                                                           <p>Eğer bu işlem bilgin dışında gerçekleştiyse lütfen bize bildir</p>
                                                           <p><b>[Site Adınız] Ekibi</b></p>
                                                       </div>
                                                       <div class="footer">
                                                           <p>İletişim: <a style="color:#54443c;" href="mailto:[Web sitesi destek maili]">[Web sitesi destek maili]</a></p>
                                               			<p>Bu mesaj otomatik olarak oluşturulmuştur. Lütfen yanıtlamayın.</p>
                                                       </div>
                                                   </div>
                                               </body>
                                               </html>
                                               """;
}