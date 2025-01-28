using System.Reflection;
using Content.Shared.Imperial.ICCVar;
using Robust.Server.Containers;
using Robust.Shared.Configuration;
using Robust.Shared.Physics;
using Robust.Shared.Physics.Components;
using Robust.Shared.Player;
using Robust.Shared.Timing;

namespace Content.Server.Imperial.BroadphaseCheck;


/// <summary>
/// Checks all players broadphase. If the player is not in the container and the broadphase is null, we update it using reflection
/// <para>
/// To do this, it would be enough to simply put a try-catch in the engine, so when the wizards fix it, this system will need to be removed.
/// </para>
/// </summary>
public sealed partial class BroadphaseCheckSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _timing = default!;
    [Dependency] private readonly MetaDataSystem _metaDataSystem = default!;
    [Dependency] private readonly ContainerSystem _containerSystem = default!;
    [Dependency] private readonly EntityLookupSystem _entityLookupSystem = default!;
    [Dependency] private readonly IConfigurationManager _configurationManager = default!;

    private TimeSpan _updateRate = TimeSpan.FromSeconds(1);
    private TimeSpan _nextUpdate = TimeSpan.Zero;
    private bool _enabled = false;


    public override void Initialize()
    {
        base.Initialize();

        _enabled = _configurationManager.GetCVar(ICCVars.BroadphaseCheckEnabled);
        _updateRate = TimeSpan.FromSeconds(_configurationManager.GetCVar(ICCVars.BroadphaseCheckUpdateRate));

        _configurationManager.OnValueChanged(ICCVars.BroadphaseCheckEnabled, newVal => _enabled = newVal);
        _configurationManager.OnValueChanged(ICCVars.BroadphaseCheckUpdateRate, newVal => _updateRate = TimeSpan.FromSeconds(newVal));
    }


    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        if (!_enabled) return;
        if (_timing.CurTime <= _nextUpdate) return;

        _nextUpdate = _timing.CurTime + _updateRate;

        var enumerator = EntityQueryEnumerator<TransformComponent, PhysicsComponent, FixturesComponent, ActorComponent>();
        while (enumerator.MoveNext(out var uid, out var transformComponent, out var _, out var _, out var _))
        {
            if (_entityLookupSystem.TryGetCurrentBroadphase(transformComponent, out var _)) continue;

            var metaData = MetaData(uid);

            if ((metaData.Flags & MetaDataFlags.InContainer) == 0) continue;
            if (_containerSystem.TryGetContainingContainer((uid, transformComponent, metaData), out var _)) continue;

            FixEntityBroadphaseTree(uid, transformComponent);
        }
    }

    #region Public API

    public void FixEntityBroadphaseTree(EntityUid target, TransformComponent? transformComponent = null)
    {
        if (!Resolve(target, ref transformComponent))
            return;

        _metaDataSystem.RemoveFlag(target, MetaDataFlags.InContainer);
        _containerSystem.AttachParentToContainerOrGrid((target, transformComponent));

        // Change transformComponent.Broadphase with reflection because it iternal
        var fields = transformComponent.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var field in fields)
        {
            if (field.Name != "Broadphase") continue;

            field.SetValue(transformComponent, null);
        }

        _entityLookupSystem.FindAndAddToEntityTree(target, true, transformComponent);
    }

    #endregion
}
