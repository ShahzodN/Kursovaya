Данное веб приложение оказывает услуги бронирования мест в гостиницах для обычных пользователей и помогает администрацию гостиницы c управлением бизнеса.

Пользовательский интерфейс сайта будет подстраиваться в зависимости от того, кто авторизовался. Имеется одна общая страница для авторизации. Пользователь сайта вводит свое имя пользователя и пароль. Система, смотря на роль пользователя пытающийся авторизоваться, перенаправить его в доступную ему область.

Существует 3 роля:
* AppAdmin – администратор приложений
* HotelAdmin – администратор гостиницы
* Visitor – посетитель, заказчик
 
Для пользователей доступны такие функции:
* Просмотр всех доступных номера гостиниц в выбранном городе.
* Отмена забронированного номера.
* Возможность оставить отзыв и оценку (от 1 до 5) к тем местам в котором он был.
* Изменить данные своего аккаунта.
* Сбросить пароль от аккаунта при потере доступа к ним.
* Фильтровать цены гостиниц и предлагаемые услуги.

 

Поиск гостиницы, цены бронирования, информация о гостинице открыто даже не авторизованным пользователям. Если не авторизованный пользователь хочет бронировать место в гостинице, то он попадет в страницу для авторизации. После успешной авторизации, произойдет бронирование.
Для администрации гостиницы доступны такие функции:

* Полная информация о сотрудниках
	* График работы.
	* Личные данные (доступна только для администратора).
* История посещений клиентов.
* Добавить/Удалить/Изменить комнат и сотрудников и выбрать администратора.


  
* Статистика и анализ данных.
	* Количество посетителей за определенный промежуток времени (день, неделя, месяц, год, кварталы года).
	* Количество новых и вернувшихся посетителей.
	* Рейтинг данной гостиницы в городе.
	* Доля посетителей по регионам.
	* Черный список (пользователи, которые забронировали, но не пришли).
 

У администратора приложений не так много функций. Он регистрирует новую гостиницу, а потом назначит в эту гостиницу администратора.

При регистрации администратора или посетителя, надо указать адрес электронной почты. Он нужен для сброса пароля, при потере доступа к аккаунту. Чтобы восстановить доступ к аккаунту надо перейти по ссылке, которая отправлена на электронную почту. Если у пользователя нет доступа к электронной почте, аккаунт не как не восстановить.

# Стек технологии
Существует много языков программирования для разработки веб-приложений. Все они имеют свои достоинства и недостатки. Но если сайт небольшой, можно выбрать любой язык программирование. В данном приложение для бэкенда выбраны C# и фреймворк ASP.NET Core 5.0. В фронтенде используется HTML5, CSS3 и библиотека jQuery написанная на JavaScript. Для хранения данных выбран реляционная база данных Microsoft SQL Server. Чтобы упростить работу с данными, в проект подключен Entity Framework Core.
Entity Framework Core является ORM-инструментом (object-relational mapping - отображения данных на реальные объекты). То есть EF Core позволяет работать базами данных, но представляет собой более высокий уровень абстракции: EF Core позволяет абстрагироваться от самой базы данных и ее таблиц и работать с данными независимо от типа хранилища. Если на физическом уровне мы оперируем таблицами, индексами, первичными и внешними ключами, но на концептуальном уровне, который нам предлагает Entity Framework, мы уже работаем с объектами.
![StackTechnologies](https://user-images.githubusercontent.com/52697255/118679274-6c3a3280-b806-11eb-8fcc-3806e849f834.png)
![Курсовая](https://user-images.githubusercontent.com/52697255/118679867-ef5b8880-b806-11eb-8935-4438968461b1.jpg)
