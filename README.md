1-Docker Desktop 'ın kurulu olması gerekiyor.

2-docker-compose projesini sağ tıklayıp open in terminal seçiyoruz.

3-docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
Yukarıdaki komutu çalıştırdığımızda veritabanları pgadmin ve rabbitmq docker üzerinde kuruluyor.

4-Catalog.Infrastructure projesinde Update-Database komutu çalıştırılmalıdır

5-PgAdmin 'e giriş yapmak için http://localhost:5050 login ekranı

Kullanıcı Adı = burhan_yildirim@windowslive.com
Şifre = admin1234

6-PgAdmin 'e giriş yapıldıktan sonra Add New Server > General Name = contactdb Connection > Host = contactdb

Username = admin Password = admin1234