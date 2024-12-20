# OzonPackManager

Приложение для удобной комплектации заказов Ozon по модели FBS. Опционально можно подключить сканер баркодов и термопринтер для печати маркировочных этикеток.

## Начало работы

Эти инструкции предоставят вам копию проекта, работающую на локальной машине для целей разработки и тестирования.

### Для чего это нужно?

В процессе сборки заказа часто возникают следующие основные ошибки:

* Сборщик положил не тот товар в посылку. Упаковочный лист не дает представления как выглядит товар.
* Сборщик ошибочно упаковал похожий по артикулу/упаковке товар.
* Сборщик неверно наклеил маркировочные этикетки на собранные посылки.

Использование сканера штрихкодов при сборке позволяет исключить ошибку в выборе товара для комплектации. Печать маркировочных этикеток "на лету" решает проблему неверной маркировки.

### Авторизация

Для авторизации и работы с API Ozon в файле Auth.xml необходимо заполнить поля ClientID и ApiKey. Убедитесь, что сгенерированный ApiKey имеет достаточные права доступа.


### Использование

* Для загрузки задания на сборку выберите нужную дату отгрузки. 
* Для подтверждения обработки товара в заказе сканируйте штрих код товара. 
* По завершению задачи будет сформирована и напечатана этикетка. Заказ переходит из статуса "Ожидает сборки" в "Ожидает отгрузки".

![screenshot](https://vitoricci.ru/tmp/screen.png)


### Оборудование
Использование дополнительных устройств является желательным, но не обязательным для работы приложения.
* Любой принтер этикеток. Необходимо установить принтером по умолчанию.
* Беспроводной сканер бар кодов Zebra/Symbol 


## Обзор решения

Решение состоит из 3 проектов:

* OzomPackManager - основное WinForms приложение;
* Ozon - библиотека для работы с OzonApi;
* OzonTests - Unit тесты.
* Zebra - билблиотека для работы со сканерами штрихкодов Zebra                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
