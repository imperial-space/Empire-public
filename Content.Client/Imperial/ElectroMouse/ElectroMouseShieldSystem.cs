using Robust.Client.GameObjects;
using Content.Shared.Imperial.ElectroMouseShield.Components;
using Robust.Shared.Utility;

namespace Content.Client.Imperial.ElectroMouseShield.Systems;

public sealed partial class ElectroMouseShieldSystem : EntitySystem
{
    private int _layer;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ElectroMouseShieldComponent, ComponentInit>(OnComponentInit);
        SubscribeLocalEvent<ElectroMouseShieldComponent, ComponentShutdown>(OnComponentShutdown);
    }
    private void OnComponentInit(EntityUid uid, ElectroMouseShieldComponent component, ComponentInit args)
    {
        if (!TryComp<SpriteComponent>(uid, out var spriteComponent))
            return;
        _layer = spriteComponent.AddLayer(new SpriteSpecifier.Rsi(new ResPath("Objects/Magic/magicactions.rsi"), "shield"), 1);
        spriteComponent.LayerMapSet(Shield.Key, _layer);
    }
    private void OnComponentShutdown(EntityUid uid, ElectroMouseShieldComponent component, ComponentShutdown args)
    {
        if (!TryComp<SpriteComponent>(uid, out var spriteComponent))
            return;
        spriteComponent.RemoveLayer(_layer);
    }
    private enum Shield
    {
        Key,
    }
}
