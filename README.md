
# Library-Application
**Project Year: 2020**

**Cloud storage library app for me**

## Genel Bakış

Library-Application, kişisel kitap arşivinizi yönetmek için geliştirilen bir Windows masaüstü uygulamasıdır. Uygulama, Firebase Cloud altyapısını kullanarak kitap bilgilerinizi bulutta saklamanızı sağlar. Tüm veriler, FireSharp kütüphanesi aracılığıyla Firebase'e iletilir ve alınır.

## Özellikler

- Kitap ekleme, silme, düzenleme
- Kitap detaylarını görüntüleme
- Bulut tabanlı veri saklama (Firebase)
- Arama ve filtreleme fonksiyonları
- Windows Forms arayüzü

## Proje Yapısı

```
Library-Application/
│
├── library.sln               # Visual Studio çözüm dosyası
├── LICENSE                   # MIT Lisansı
├── README.md                 # Proje açıklaması
├── library/                  # Ana uygulama kaynak kodları
│   ├── Program.cs            # Uygulamanın başlangıç noktası
│   ├── Form1.cs              # Ana form ve kitap işlemleri
│   ├── DetailsForm.cs        # Kitap detaylarını gösteren form
│   ├── AddEditForm.cs        # Kitap ekleme/düzenleme formu
│   ├── Properties/           # Uygulama ayarları ve kaynak dosyaları
│   │   └── AssemblyInfo.cs   # Derleme bilgileri
│   ├── Resources/            # Görsel ve veri dosyaları
│   └── obj/                  # Derleme çıktıları (otomatik oluşturulur)
└── .vs/                      # Visual Studio çalışma alanı dosyaları
```

## Gereksinimler

- .NET Framework 4.7.2
- Visual Studio 2019 veya üzeri
- Firebase hesabı (Realtime Database)
- FireSharp NuGet paketi

## Kurulum

1. Projeyi klonlayın:
    ```
    git clone https://github.com/sonmezarda/Library-Application.git
    ```
2. Visual Studio ile `library.sln` dosyasını açın.
3. `Form1.cs` dosyasındaki Firebase ayarlarını kendi veritabanınıza göre düzenleyin:
    ```csharp
    IFirebaseConfig fcon = new FirebaseConfig
    {
        AuthSecret = "TOKEN",
        BasePath = "URL"
    };
    ```
4. Bağımlılıkları yükleyin (FireSharp).
5. Uygulamayı derleyip çalıştırın.

## Kullanım

- Kitap eklemek için ana formdan ilgili alanları doldurup kaydedebilirsiniz.
- Kitap detaylarını görmek için bir kitabı seçip "Detaylar" butonuna tıklayın.
- Kitapları arayabilir, silebilir veya düzenleyebilirsiniz.

## Lisans

Bu proje MIT lisansı ile lisanslanmıştır. Ayrıntılar için `LICENSE` dosyasını inceleyebilirsiniz.

---

Daha fazla detay istersen veya kodun belirli bir parçası hakkında açıklama istiyorsan lütfen bana belirt!
