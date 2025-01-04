using Robust.Shared.Timing;
using Content.Shared.SubFloor;
using Content.Client.SubFloor;
using Robust.Client.Animations;
using Robust.Client.GameObjects;
using Content.Shared.Imperial.ElectroMouse.Components;

namespace Content.Client.Imperial.ElectroMouse;

public sealed class ElectroMouseSystem : EntitySystem
{
    [Dependency] private readonly IGameTiming _gameTiming = default!;
    [Dependency] private readonly EntityLookupSystem _lookup = default!;
    [Dependency] private readonly AnimationPlayerSystem _animation = default!;
    [Dependency] private readonly SharedAppearanceSystem _appearance = default!;
    [Dependency] private readonly SharedTransformSystem _transform = default!;
    public const LookupFlags Flags = LookupFlags.Static | LookupFlags.Sundries | LookupFlags.Approximate;
    private const string TRayAnimationKey = "trays";
    private const double AnimationLength = 0.3;
    public const float SubfloorRevealAlpha = 0.8f;
    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        if (!_gameTiming.IsFirstTimePredicted)
            return;

        var xformQuery = GetEntityQuery<TransformComponent>();
        HashSet<Entity<SubFloorHideComponent>> inRange;

        var query = EntityQueryEnumerator<ElectroMouseComponent>();
        while (query.MoveNext(out var uid, out var component))
        {
            var playerPos = _transform.GetWorldPosition(Transform(uid), xformQuery);
            var playerMap = Transform(uid).MapID;

            bool canSee = false;

            if (component.IsSpeed)
                canSee = true;

            var range = component.Range;
            inRange = new HashSet<Entity<SubFloorHideComponent>>();

            if (canSee)
            {
                _lookup.GetEntitiesInRange(playerMap, playerPos, range, inRange, flags: Flags);

                foreach (var (ent, comp) in inRange)
                {
                    if (comp.IsUnderCover)
                        EnsureComp<TrayRevealedComponent>(ent);
                }
            }

            var revealedQuery = AllEntityQuery<TrayRevealedComponent, SpriteComponent>();
            var subfloorQuery = GetEntityQuery<SubFloorHideComponent>();

            while (revealedQuery.MoveNext(out var ent, out _, out var sprite))
            {
                // Revealing
                // Add buffer range to avoid flickers.
                if (subfloorQuery.TryGetComponent(ent, out var subfloor) &&
                    inRange.Contains((ent, subfloor)))
                {
                    // Due to the fact client is predicting this server states will reset it constantly
                    if ((!_appearance.TryGetData(ent, SubFloorVisuals.ScannerRevealed, out bool value) || !value) &&
                        sprite.Color.A > SubfloorRevealAlpha)
                    {
                        sprite.Color = sprite.Color.WithAlpha(0f);
                    }

                    SetRevealed(ent, true);

                    if (sprite.Color.A >= SubfloorRevealAlpha || _animation.HasRunningAnimation(ent, TRayAnimationKey))
                        continue;

                    _animation.Play(ent, new Animation()
                    {
                        Length = TimeSpan.FromSeconds(AnimationLength),
                        AnimationTracks =
                        {
                            new AnimationTrackComponentProperty()
                            {
                                ComponentType = typeof(SpriteComponent),
                                Property = nameof(SpriteComponent.Color),
                                KeyFrames =
                                {
                                    new AnimationTrackProperty.KeyFrame(sprite.Color.WithAlpha(0f), 0f),
                                    new AnimationTrackProperty.KeyFrame(sprite.Color.WithAlpha(SubfloorRevealAlpha), (float) AnimationLength)
                                }
                            }
                        }
                    }, TRayAnimationKey);
                }
                // Hiding
                else
                {
                    // Hidden completely so unreveal and reset the alpha.
                    if (sprite.Color.A <= 0f)
                    {
                        SetRevealed(ent, false);
                        RemCompDeferred<TrayRevealedComponent>(ent);
                        sprite.Color = sprite.Color.WithAlpha(1f);
                        continue;
                    }

                    SetRevealed(ent, true);

                    if (_animation.HasRunningAnimation(ent, TRayAnimationKey))
                        continue;

                    _animation.Play(ent, new Animation()
                    {
                        Length = TimeSpan.FromSeconds(AnimationLength),
                        AnimationTracks =
                        {
                            new AnimationTrackComponentProperty()
                            {
                                ComponentType = typeof(SpriteComponent),
                                Property = nameof(SpriteComponent.Color),
                                KeyFrames =
                                {
                                    new AnimationTrackProperty.KeyFrame(sprite.Color, 0f),
                                    new AnimationTrackProperty.KeyFrame(sprite.Color.WithAlpha(0f), (float) AnimationLength)
                                }
                            }
                        }
                    }, TRayAnimationKey);
                }
            }

        }
    }

    private void SetRevealed(EntityUid uid, bool value)
    {
        _appearance.SetData(uid, SubFloorVisuals.ScannerRevealed, value);
    }

}
