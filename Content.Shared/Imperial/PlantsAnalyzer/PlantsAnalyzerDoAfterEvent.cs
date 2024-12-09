using Content.Shared.DoAfter;
using Robust.Shared.Serialization;

namespace Content.Shared.Imperial.PlantsAnalyzer;

[Serializable, NetSerializable]
public sealed partial class PlantsAnalyzerDoAfterEvent : SimpleDoAfterEvent
{
}
