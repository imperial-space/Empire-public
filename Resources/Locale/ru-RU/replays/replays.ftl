# Loading Screen
replay-loading = Загрузка ({$cur}/{$total})
replay-loading-reading = Чтение файлов
replay-loading-processing = Обработка файлов
replay-loading-spawning = Создание сущностей
replay-loading-initializing  = Инициализация сущностей
replay-loading-starting = Начальные сущности
replay-loading-failed = Не удалось загрузить повтор:
{$reason}
replay-loading-retry = Попробуйте загрузить с большей устойчивостью к исключениям - МОЖЕТ ВЫЗВАТЬ ОШИБКИ!
replay-loading-cancel = Отменить
# Main Menu
replay-menu-subtext = Клиент воспроизведения
replay-menu-load = Загрузить выбранный повтор
replay-menu-select = Выбрать повтор
replay-menu-open = Открыть папку с повторами
replay-menu-none = Повторы не найдены.
# Main Menu Info Box
replay-info-title = Информация о воспроизведении
replay-info-none-selected  = Повтор не выбран
replay-info-invalid = [color=red]Выбран неверный повтор[/color]
replay-info-info =
    { "[" }color=gray]Выбрано:[/color]  { $name } ({ $file })
    { "[" }color=gray]Время:[/color]   { $time }
    { "[" }color=gray]ID раунда:[/color]   { $roundId }
    { "[" }color=gray]Продолжительность:[/color]   { $duration }
    { "[" }color=gray]ForkId:[/color]   { $forkId }
    { "[" }color=gray]Version:[/color]   { $version }
    { "[" }color=gray]Engine:[/color]   { $engVersion }
    { "[" }color=gray]Type Hash:[/color]   { $hash }
    { "[" }color=gray]Comp Hash:[/color]   { $compHash }
# Replay selection window
replay-menu-select-title = Выбрать повтор
# Replay related verbs
replay-verb-spectate = Наблюдать
# command
cmd-replay-spectate-help  = Использование: replay_spectate [сущность (опционально)]
cmd-replay-spectate-desc  = Присоединяет или отсоединяет локального игрока к данному идентификатору сущности.
cmd-replay-spectate-hint  = Необязательный EntityUid
cmd-replay-toggleui-desc  = Переключает пользовательский интерфейс управления воспроизведением.
