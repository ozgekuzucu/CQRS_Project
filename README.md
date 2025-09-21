# 🚙 RentiGo - Yapay Zeka Destekli Araç Kiralama Platformu

RentiGo, **ASP.NET Core 9.0** üzerinde **CQRS (Command Query Responsibility Segregation)** uygulanarak geliştirilmiş bir **araç kiralama yönetim platformudur**. Proje; kullanıcıların kolayca rezervasyon yapabilmesini, yöneticilerin ise araç, kategori, lokasyon ve rezervasyon yönetimini etkin biçimde gerçekleştirmesini sağlar.  

## 🎯 Genel Amaç

Bu uygulamanın temel hedefi;  
- **CQRS mimarisi** ile okunabilir ve sürdürülebilir bir yapı kurmak,  
- **RapidAPI ve Yapay Zeka** entegrasyonlarıyla gerçek zamanlı veri akışı sağlamak,  
- Kullanıcıların rezervasyon sürecinde araç bulmasını kolaylaştırmak ve  
- Admin paneli üzerinden tüm yönetimsel işlemleri tek noktadan yapabilmektir.

## 🌐 API Kullanımları

Proje kapsamında birden fazla servis entegre edilmiştir. Her bir API, farklı işlevleri destekleyerek sistemin hem kullanıcı hem de yönetici tarafında daha işlevsel hale gelmesini sağlamaktadır.

**⛽ GasPrice API (RapidAPI)**  
  - Türkiye'deki anlık yakıt fiyatlarını (benzin, motorin, LPG) almak için kullanıldı.  
  - Bu sayede rezervasyon sırasında **mesafe + yakıt maliyeti** hesaplanırken güncel veriler dikkate alınmaktadır.  
  - **Endpoint:** `https://gas-price.p.rapidapi.com/europeanCountries`

**🗺️ Google Maps Directions API**  
 - Türkiye’deki şehirlerin listelenmesi için kullanıldı.  
 - Kullanıcıdan alınan adres veya mekan adını coğrafi koordinatlara dönüştürmek için kullanıldı.  
 - Örneğin “Sabiha Gökçen Havalimanı” yazıldığında, bu API sayesinde adres bilgisi ve koordinatlar elde edilerek rezervasyon sistemine entegre edilmiştir.  
- **Endpoint:** `https://google-map-places.p.rapidapi.com`

**💱 Exchange Rate API (RapidAPI)**  
 - Döviz kurunu EUR-TRY çevirmek için kullanıldı.  
 - Yakıt maliyetlerinin TL bazlı hesaplanabilmesi için entegrasyon sağlanmıştır.  
- **Endpoint:** `https://exchange-rates7.p.rapidapi.com/convert?base=EUR&target=TRY`

## 🤖 Yapay Zeka Entegrasyonları
- **📄 Akıllı Cevap Üretimi (Gemini AI)**
  - **Amaç:** Kullanıcılardan gelen mesajlara otomatik, profesyonel ve kısa yanıtlar üretir
  - **Notlar:** Mesajın dilini tespit eder, kısa ve öz yanıt üretir, admin bildirimi de mail üzerinden iletilir.
    
- **🚗 Araç Önerisi (Gemini AI)**
-**Amaç:** Kullanıcının araç kiralama isteğine göre en uygun araçları önerir.

### 👤 Kullanıcı Arayüzü (UI)
- Seçilen tarih aralığına göre **müsait araçların listelenmesi**  
- **Pick-Up / Drop-Off** bilgileri RapidAPI’den çekilerek saklanır  
- 2 lokasyon seçildiğinde **mesafe + yakıt maliyeti** hesaplanır  
- Rezervasyon yapılabilir  
- İletişim formu doldurulabilir → mesajlar admin paneline düşer
  
![ui](/images/full-page.jpeg)
![ui](/images/reservation-form-1.png)
![ui](/images/cars.JPG)
![ui](/images/statistic.jpeg)
![ui](/images/mail.png)
![ui](/images/contact-form.png)

### 🛠 Admin Paneli
- Araç, kategori, rezervasyon, lokasyon ve kullanıcı yönetimi  
- **AI Destekli Araç Öneri Asistanı**  
- **Dashboard** ekranında günlük benzin, motorin ve LPG fiyatları  
- Gelen mesajlara **AI ile otomatik yanıt**  

![admin-paneli](/images/admin-dashboard.jpeg)

![admin-paneli](/images/admin-car-list.jpeg)

![admin-paneli](/images/admin-add-location-from-api.jpeg)

![admin-paneli](/images/admin-add-reservation.jpeg)

![admin-paneli](/images/admin-employee-list.jpeg)

![admin-paneli](/images/admin-location-list.jpeg)

![admin-paneli](/images/admin-reservation-list.jpeg)

![admin-paneli](/images/ai-car.jpeg)

![admin-paneli](/images/ai-car2.jpeg)

---

## 🧩 Kullanılan Teknolojiler
- **ASP.NET Core 9.0**
- **CQRS Pattern**  
- **Entity Framework Core** → ORM  
- **MS SQL Server** → Veritabanı  
- **RapidAPI** → Lokasyon, mesafe ve yakıt fiyatı servisleri  
- **Hugging Face API** → AI tabanlı yanıt ve araç önerileri  
- **MailKit** → Kullanıcı mesajlarına otomatik mail cevabı  
- **Bootstrap 5 + Cental Template** → Responsive UI  

