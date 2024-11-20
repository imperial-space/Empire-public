using Content.Shared.Actions;
using Content.Shared.Chemistry.Components;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.Borgs
{
    [RegisterComponent, NetworkedComponent]
    public sealed partial class BorgHypoComponent : Component
    {
        [DataField("Solutions")]
        public List<Solution> Solutions = new List<Solution>();

        public int CurrentIndex = 0;

        [ViewVariables(VVAccess.ReadWrite)]
        public bool UiUpdateNeeded;

        [ViewVariables(VVAccess.ReadWrite)]
        public string CurrentReagentName = "бикаридин";

        [DataField]
        public EntProtoId Action = "ChangeReagent";

        [DataField]
        public EntityUid? ActionEntity;
    }
    [Serializable, NetSerializable]
    public sealed class BorgHypoComponentState : ComponentState
    {
        public readonly bool UiUpdateNeeded;
        public readonly string CurrentReagentName;

        public BorgHypoComponentState(bool uiUpdateNeeded, string currentReagenName)
        {
            UiUpdateNeeded = uiUpdateNeeded;
            CurrentReagentName = currentReagenName;
        }
    }
    public sealed partial class ChangeReagentAction : InstantActionEvent { }
}
