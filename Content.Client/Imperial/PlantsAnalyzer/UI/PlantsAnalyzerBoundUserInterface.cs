using Content.Shared.Imperial.PlantsAnalyzer;
using JetBrains.Annotations;
using Robust.Client.UserInterface;

namespace Content.Client.Imperial.PlantsAnalyzer.UI
{
    [UsedImplicitly]
    public sealed class PlantsAnalyzerBoundUserInterface : BoundUserInterface
    {
        [ViewVariables]
        private PlantsAnalyzerWindow? _window;

        public PlantsAnalyzerBoundUserInterface(EntityUid owner, Enum uiKey) : base(owner, uiKey)
        {
        }

        protected override void Open()
        {
            base.Open();

            _window = this.CreateWindow<PlantsAnalyzerWindow>();

            _window.Title = EntMan.GetComponent<MetaDataComponent>(Owner).EntityName;
        }

        protected override void ReceiveMessage(BoundUserInterfaceMessage message)
        {
            if (_window == null)
                return;

            if (message is not PlantsAnalyzerScannedUserMessage cast)
                return;

            _window.Populate(cast);
        }
    }
}
