namespace Content.Server.Imperial.GasMaskFullProtection;


[RegisterComponent]
[Access(typeof(GasMaskFullProtectionSystem))]
public sealed partial class GasMaskFullProtectionUserComponent : Component
{
    [DataField]
    public HashSet<EntityUid> GasMasks = new();
}
