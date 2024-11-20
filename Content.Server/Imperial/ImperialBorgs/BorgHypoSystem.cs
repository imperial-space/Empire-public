using Content.Server.Chemistry.Components;
using Content.Server.Chemistry.Containers.EntitySystems;
using Content.Server.Chemistry.EntitySystems;
using Content.Shared.Actions;
using Content.Shared.Borgs;
using Content.Shared.Chemistry.EntitySystems;
using Content.Shared.Chemistry.Reagent;
using Content.Shared.Verbs;
using Robust.Shared.Prototypes;

namespace Content.Server.Borgs
{
    public sealed class BorgHypoSystem : EntitySystem
    {
        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
        [Dependency] private readonly SolutionContainerSystem _solutionSystem = default!;

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<BorgHypoComponent, GetVerbsEvent<AlternativeVerb>>(AddSwitchVerb);
            SubscribeLocalEvent<BorgHypoComponent, GetItemActionsEvent>(OnGetActions);
            SubscribeLocalEvent<BorgHypoComponent, ChangeReagentAction>(OnReagentChange);
        }

        private void OnGetActions(EntityUid uid, BorgHypoComponent component, GetItemActionsEvent args)
        {
            args.AddAction(ref component.ActionEntity, component.Action);
        }

        private void OnReagentChange(EntityUid uid, BorgHypoComponent component, ChangeReagentAction args)
        {
            if (args.Handled) { return; }
            SwitchReagent(uid, component);
            args.Handled = true;
        }

        private void AddSwitchVerb(EntityUid uid, BorgHypoComponent component, GetVerbsEvent<AlternativeVerb> args)
        {
            if (!args.CanInteract || !args.CanAccess)
                return;

            if (component.Solutions.Count <= 1)
                return;

            AlternativeVerb verb = new()
            {
                Act = () =>
                {
                    SwitchReagent(uid, component);
                },
                Text = Loc.GetString("borghypo-switchreagent"),
                Priority = 1
            };
            args.Verbs.Add(verb);
        }

        private void SwitchReagent(EntityUid uid, BorgHypoComponent component)
        {
            if (!TryComp<SolutionRegenerationComponent>(uid, out var solutionRegenerationComponent))
            {
                return;
            }

            if (component.CurrentIndex == component.Solutions.Count)
            {
                component.CurrentIndex = 0;
            }
            else
            {
                component.CurrentIndex++;
            }

            if (!_solutionSystem.TryGetSolution(uid, solutionRegenerationComponent.SolutionName, out var solution))
            {
                return;
            }

            var newSolution = component.Solutions[component.CurrentIndex];
            var primaryId = newSolution.GetPrimaryReagentId();
            if (primaryId == null)
            {
                return;
            }

            if (!_prototypeManager.TryIndex(primaryId.Value.ToString(), out ReagentPrototype? proto) || proto == null)
            {
                return;
            }

            solution.Value.Comp.Solution.RemoveAllSolution();

            var generated = solutionRegenerationComponent.Generated;

            generated.RemoveAllSolution();
            generated.AddSolution(newSolution, _prototypeManager);

            component.CurrentReagentName = proto.LocalizedName;
            component.UiUpdateNeeded = true;
            Dirty(uid, component);
        }
    }
}
