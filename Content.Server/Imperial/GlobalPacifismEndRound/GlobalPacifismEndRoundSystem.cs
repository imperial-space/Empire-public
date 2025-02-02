using Content.Shared.CombatMode.Pacification;
using Content.Shared.GameTicking;
using Content.Shared.Mind.Components;

namespace Content.Server.Imperial.GlobalPacifismEndRound;

public sealed class GlobalPacifismRoundEndSystem : EntitySystem
{

    private bool _isEnabled = true;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<RoundEndMessageEvent>(OnRoundEnded);
    }

    private void OnRoundEnded(RoundEndMessageEvent ev)
    {
        if (!_isEnabled) return;
        var mindContainerEnumerator = EntityQueryEnumerator<MindContainerComponent>();

        while (mindContainerEnumerator.MoveNext(out var mindContainer))
        {
            try
            {
                EnsureComp<PacifiedComponent>(mindContainer.Owner);
            }
            catch (Exception e)
            {
                Log.Error($"Error adding PacifiedComponent to {mindContainer.Owner} uid: {e}");
            }
        }
    }
}
