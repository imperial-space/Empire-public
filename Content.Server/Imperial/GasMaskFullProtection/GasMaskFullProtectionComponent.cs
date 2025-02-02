namespace Content.Server.Imperial.GasMaskFullProtection;


[RegisterComponent]
[Access(typeof(GasMaskFullProtectionSystem))]
public sealed partial class GasMaskFullProtectionComponent : Component
{
    [DataField]
    public float TolerableRadiation = 0;
}
