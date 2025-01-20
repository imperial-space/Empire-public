using Content.Shared.Actions;
using Content.Shared.Clothing;
using Content.Shared.Clothing.Components;
using Content.Shared.Inventory;
using Content.Shared.Item.ItemToggle;
using Content.Shared.Toggleable;

namespace Content.Shared.Clothing.NVDSystem;
[RegisterComponent, NetworkedComponent]
public sealed class ToggleNVDSystem : NVDSystem
{
    [Dependency] private readonly SharedActionsSystem _actions = default!;
    [Dependency] private readonly ItemToggleSystem _toggle = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ToggleClothingComponent, GetItemActionsEvent>(OnGetActions);
        SubscribeLocalEvent<ToggleNVDComponent, ToggleActionEvent>(OnToggleAction);
        SubscribeLocalEvent<ToggleNVDComponent, ClothingGotUnequippedEvent>(OnUnequipped);
        SubscribeLocalEvent<EyeComponent, ToggleNVDActionEvent>(OnToggleNVD);
    }


    private void OnToggleAction(Entity<ToggleNVDComponent> ent, ref ToggleActionEvent args)
    {
        args.Handled = _toggle.Toggle(ent.Owner, args.Performer);
    }

    private void OnUnequipped(Entity<ToggleNVDComponent> ent, ref ClothingGotUnequippedEvent args)
    {
        if (ent.Comp.DisableOnUnequip)
            _toggle.TryDeactivate(ent.Owner, args.Wearer);
    }

    {
        private void OnToggleNVD(EntityUid uid, EyeComponent component, ToggleNVDActionEvent args)

            if (args.Handled)
                return;

            _contentEye.RequestToggleLight(uid, component);
            args.Handled = true;
    }
}


