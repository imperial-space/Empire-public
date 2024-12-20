using Content.Shared.Imperial.Pixelize;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Player;

namespace Content.Client.Imperial.Pixelize;


/// <summary>
/// Система, добавляющая оверлей на сущность
/// </summary>
public sealed partial class PixelizeSystem : EntitySystem
{
    [Dependency] private readonly IOverlayManager _overlayManager = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;

    private PixelizeOverlay _overlay = default!;


    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PixelizeComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<PixelizeComponent, ComponentShutdown>(OnShutdown);

        SubscribeLocalEvent<PixelizeComponent, LocalPlayerAttachedEvent>(OnPlayerAttached);
        SubscribeLocalEvent<PixelizeComponent, LocalPlayerDetachedEvent>(OnPlayerDetached);

        _overlay = new();
    }

    private void OnStartup(EntityUid uid, PixelizeComponent component, ComponentStartup args)
    {
        // Если сущность игрока на клиенте не равна сущности на которую добавили компонент, то не добавляем оверлей
        if (_playerManager.LocalEntity != uid) return;

        _overlayManager.AddOverlay(_overlay);
    }

    private void OnShutdown(EntityUid uid, PixelizeComponent component, ComponentShutdown args)
    {
        // Если сущность игрока на клиенте не равна сущности на которую добавили компонент, то не убираем оверлей
        if (_playerManager.LocalEntity != uid) return;

        _overlayManager.RemoveOverlay(_overlay);
    }

    private void OnPlayerAttached(EntityUid uid, PixelizeComponent component, LocalPlayerAttachedEvent args)
    {
        _overlayManager.AddOverlay(_overlay);
    }

    private void OnPlayerDetached(EntityUid uid, PixelizeComponent component, LocalPlayerDetachedEvent args)
    {
        _overlayManager.RemoveOverlay(_overlay);
    }
}
