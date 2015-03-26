## GET AD USERS

### Назначение
ПО **Get AD users** позволяет считать список пользователей с контроллера домена и сохранить их в файл для последующего импорта в IceWarp 11

### Documentation

Список пользователей сохраняется в формате:

```
#!csv

user@domain.local,1q2w3e4r5,User Name
```
где:
* user - транслитерированное имя пользователя;
* domain.local - полное имя домена;
* 1q2w3e4r5 - пароль для пользователя, устанавливаемый по-умолчанию
* User Name - Полное имя пользователя, указанное в домене для данной учетной записи


### License

The Laravel framework is open-sourced software licensed under the [MIT license](http://opensource.org/licenses/MIT)