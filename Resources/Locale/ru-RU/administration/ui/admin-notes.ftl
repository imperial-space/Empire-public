﻿# UI
admin-notes-title = Заметки о {$player}
admin-notes-new-note = Добавить заметку
admin-notes-show-more = Показать больше
admin-notes-id = Id: {$id}
admin-notes-type = Тип: {$type}
admin-notes-severity = Серьёзность: {$severity}
admin-notes-secret = Секретно
admin-notes-notsecret = Не секретно
admin-notes-expires = Истекает: {$expires}
admin-notes-expires-never = Не истекает
admin-notes-edited-never = Никогда
admin-notes-round-id = Id раунда: {$id}
admin-notes-round-id-unknown = Id раунда: неизвестно
admin-notes-created-by = Создал: {$author}
admin-notes-created-at = Дата создания: {$date}
admin-notes-last-edited-by = Последним изменил: {$author}
admin-notes-last-edited-at = Дата изменения: {$date}
admin-notes-edit = Изменить
admin-notes-delete = Удалить
admin-notes-hide = Скрыть
admin-notes-delete-confirm = Вы уверены?
admin-notes-edited = Последнее изменение от {$author} в {$date}
admin-notes-unbanned = Разбанил {$admin} в {$date}
admin-notes-message-desc = [color=white]Вы получили { $count ->
    [1] сообщение от администрации
    *[other] сообщений от администрации
} с последнего момента вашего захода на сервер [/color]
admin-notes-message-admin = From [bold]{ $admin }[/bold], written on { TOSTRING($date, "f") }:
admin-notes-message-wait = Кнопка будет доступна через {$time} секунд.
admin-notes-message-accept = Скрыть навсегда
admin-notes-message-dismiss = Скрыть временно
admin-notes-message-seen = Просмотрено
admin-notes-banned-from = В бане
admin-notes-the-server = на сервере
admin-notes-permanently = перманентно
admin-notes-for = Заметка для: {$player}
admin-notes-days = {$days} дней
admin-notes-hours = {$hours} часов
admin-notes-minutes = {$minutes} минут

# Note editor UI
admin-note-editor-title-new = Новая заметка для {$player}
admin-note-editor-title-existing = Изменение заметки {$id} для {$player} от {$author}
admin-note-editor-pop-out = Поп-аут
admin-note-editor-secret = Секрет?
admin-note-editor-secret-tooltip = Если установить этот флажок, то заметка не будет видна игроку
admin-note-editor-type-note = Заметка
admin-note-editor-type-message = Сообщение
admin-note-editor-type-watchlist = Наблюдение
admin-note-editor-type-server-ban = Сервер бан
admin-note-editor-type-role-ban = Роль бан
admin-note-editor-severity-select = Выбрать
admin-note-editor-severity-none = Нет
admin-note-editor-severity-low = Низкий
admin-note-editor-severity-medium = Средний
admin-note-editor-severity-high = Высокий
admin-note-editor-expiry-checkbox = Пермаментно?
admin-note-editor-expiry-checkbox-tooltip = Уберите флажок, что бы сделать его истекаемым
admin-note-editor-expiry-label = Истекает в:
admin-note-editor-expiry-label-params = Истекает: {$date} (через {$expiresIn})
admin-note-editor-expiry-label-expired = Истёк
admin-note-editor-expiry-placeholder = Укажите срок действия (yyyy-MM-dd HH:mm:ss)
admin-note-editor-submit = Подтвердить
admin-note-editor-submit-confirm = Вы уверены?

# Verb
admin-notes-verb-text = Заметки

# Watchlist and message login
admin-notes-watchlist = Наблюдение над {$player}: {$message}
admin-notes-new-message = Вы получили админ сообщение от {$admin}: {$message}
admin-notes-fallback-admin-name = [System]

# Admin remarks
admin-remarks-command-description = Открыть страницу админ замечаний
admin-remarks-command-error = Админ замечания были отключены
admin-remarks-title = Админ замечания

# Misc
system-user = [Система]

# Time
admin-note-button-minutes = Минуты
admin-note-button-hours = Часы
admin-note-button-days = Дни
admin-note-button-weeks = Недели
admin-note-button-months = Месяцы
admin-note-button-years = Годы
admin-note-button-centuries = Века
