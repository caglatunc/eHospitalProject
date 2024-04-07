# e-Hospital Project

## Proje Hakkında

e-Hospital, .NET 8 ve Angular 17 kullanılarak geliştirilen bir web platformudur. Kullanıcıların rolüne göre dinamik olarak değişen içeriği ile doktorlar ve hastalar kendi randevularını görebilirken, yöneticiler doktor seçimi yaparak hastalar için randevu oluşturabilirler. Sistem, güvenliği ve kullanıcı deneyimini ön planda tutarak JWT tabanlı kimlik doğrulama, refresh token mekanizması ve e-posta onayı gibi özellikler ile desteklenmiştir.

## Özellikler

- **Kullanıcı Rollerine Göre Dinamik İçerik**: Doktorlar için randevu görüntüleme, adminler için doktor seçerek randevu oluşturma.
- **Güvenli Kimlik Doğrulama**: JWT (JSON Web Tokens) ve refresh token kullanımı.
- **E-posta Onayı**: Kullanıcı oluşturulduğunda e-posta onayı gerekmektedir.
- **Şifremi Unuttum**: Kullanıcılar unuttukları şifrelerini e-posta üzerinden sıfırlayabilirler.
- **Randevu Yönetimi**: Adminler tarafından doktor seçimi ile hastalara randevu atama.

## Teknolojiler
## Backend
- **Backend**: .NET 8
- **Veritabanı:** PostgreSQL
- **Kimlik Doğrulama:** JWT (JSON Web Tokens), Refresh Token
-  Options Pattern
-  Result Pattern

### Frontend
-  **Frontend**: Angular 17
- **UI Kütüphanesi:** DevExtreme Angular

## Demo Kullanımı

Demo amaçlı erişim için, lütfen aşağıdaki bilgileri kullanın. Unutmayın ki, demo hesabı sınırlı yetkilere sahiptir ve yalnızca belirli işlevleri test etmek için tasarlanmıştır.

### Admin
- Kullanıcı Adı: admin@admin.com
- Şifre: 1

### Doktor
- Kullanıcı Adı: hkaya
- Şifre: Password12*

7 Nisan 
  -Tamamlanan randevuların ekranda statues değişmesi (Tamamlanan randevular ekrana düşmüyor?)


