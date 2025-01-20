using Content.Shared.Actions;
using Content.Shared.Clothing.EntitySystems;
using Content.Shared.Item.ItemToggle.Components;
using Content.Shared.Toggleable;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared.Clothing.Components;


[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
[Access(typeof(ToggleNVDSystem))]
public sealed partial class ToggleNVDComponent : Component
{

    [DataField(required: true)]
    public EntProtoId<InstantActionComponent> Action = string.Empty;

    [DataField, AutoNetworkedField]
    public EntityUid? ActionEntity;

    [DataField]
    public bool DisableOnUnequip;

    [DataField]
    public bool MustEquip = true;
}

[ByRefEvent]
public record struct ToggleClothingCheckEvent(EntityUid User, bool Cancelled = false);
