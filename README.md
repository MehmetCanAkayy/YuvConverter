# YuvConverter
![image](https://user-images.githubusercontent.com/24396577/54497019-be215e00-4906-11e9-8d5f-ae14b75ec126.png)

YUV uzantılı dosyaların 4:4:4 , 4:2:2 , 4:2:0 şeklinde.Bu dosyaların Y değerlerinin düzgün şekilde okunup daha sonrasında .bmp şeklinde
kayıt edildikten sonra video oynatıcıda gösterimine dayanmaktadır.Bu video ayarlarında bu okunan dosyalara uygun şekilde render yapılıp en ve boy’a göre hazırlanması.<br/>
Bunların hepsinin arayüze yansıtılıp kullanıcı kolaylığı sağlamasıda bulunmaktadır.




![image](https://user-images.githubusercontent.com/24396577/54497191-bc589a00-4908-11e9-9126-aaff8785c0f8.png)

Arayüzde ilk önce browse kısmınına tıklayarak render ediceğimiz dosyayı yüklüyoruz.<br/>
Format kısmında yüklediğimiz dosyanın tipini seçiyoruz.Ona göre bir işlem gerçekleştirmek için(YUV444, YUV422, YUV420).<br/>
Seçtiğimiz dosyayayı uygun piksel değerlerini height ve width kısımlarına giriyoruz.<br/>
Start dediğimiz zaman dosyanın ilk frame’i ekrana getirilmiş oluyor.<br/>
Yukarıdaki labellarda gösterilen total frame sayısı dosyanın start işleminde .bmp ler oluşturulup kayıt edilmiş ve toplam frame sayısı arayüze yansımış oluyor.<br/>
Startın yanındaki oynatma butonuna basınca sadece Y değerine göre işlenmiş .bmp dosyaları oynatılıyor.<br/>
Bu sırada yandaki butonlar ile ileri ve geri hızlı seçilde sarılabiliyor.Durdurulma işlemi de yapılabiliyor.<br/>
