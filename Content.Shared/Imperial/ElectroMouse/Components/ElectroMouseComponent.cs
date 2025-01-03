using System.Numerics;
using Content.Shared.FixedPoint;
using Content.Shared.Store;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.Imperial.ElectroMouse.Components;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class ElectroMouseComponent : Component
{
    public Vector2? Coordinates;

    [ValidatePrototypeId<EntityPrototype>]
    public readonly string ShopId = "ActionElectroMouseShop";

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public string ElevationSound { get; set; } = "/Audio/Announcements/bloblarm.ogg";

    public AudioParams Params = AudioParams.Default.WithVolume(5f);

    public bool IsChanged = false;

    [DataField, ViewVariables(VVAccess.ReadWrite), AutoNetworkedField]
    public int Range = 5;

    [ViewVariables(VVAccess.ReadWrite), AutoNetworkedField]
    public bool IsSpeed = false;

    [ViewVariables(VVAccess.ReadWrite)]
    public List<EntityUid> Harvested = new List<EntityUid>();

    [ViewVariables(VVAccess.ReadWrite)]

    public FixedPoint2 Energy = 1;

    [ViewVariables(VVAccess.ReadWrite)]
    public bool CanBattery = false;

    [ViewVariables(VVAccess.ReadWrite)]
    public bool CanAPC = false;

    [ViewVariables(VVAccess.ReadWrite)]
    public bool IsActiveShield = false;

    [ViewVariables(VVAccess.ReadOnly)]
    public TimeSpan TimeUtil;

    [ViewVariables(VVAccess.ReadOnly)]
    public TimeSpan TimeUtilSpeed;

    [ViewVariables(VVAccess.ReadWrite), DataField]
    public float Duration = 2.0f;

    [DataField("stolenEssenceCurrencyPrototype", customTypeSerializer: typeof(PrototypeIdSerializer<CurrencyPrototype>))]
    public string StolenEnergyCurrencyPrototype = "StolenEnergy";

    /// <summary>
    /// Prototype to spawn when the entity dies.
    /// </summary>
    [DataField("spawnOnDeathPrototype", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string SpawnOnDeathPrototype = "AnomalyCoreElectroMouse";

    [DataField("harvestDebuffs")]
    public Vector2 HarvestDebuffs = new(5, 5);

    [DataField("blinkSound"), ViewVariables(VVAccess.ReadWrite)]
    public SoundSpecifier DashSound = new SoundPathSpecifier("/Audio/Magic/blink.ogg")
    {
        Params = AudioParams.Default.WithVolume(5f)
    };
    [DataField, ViewVariables(VVAccess.ReadWrite)]

    public int DashEnergy = 2;
    [ViewVariables(VVAccess.ReadWrite), DataField("overloadCost")]

    public FixedPoint2 OverloadCost = -40;

    [DataField("overloadDebuffs")]

    public Vector2 OverloadDebuffs = new(3, 8);

    [ViewVariables(VVAccess.ReadWrite), DataField("overloadRadius")]

    public float OverloadRadius = 5f;

    [ViewVariables(VVAccess.ReadWrite), DataField("overloadZapRadius")]

    public float OverloadZapRadius = 2f;

    [DataField] public EntityUid? Action;
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public int EmpRadius = 5;
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public int HealingStrength = 10;

    [DataField("sparkSound")]
    public SoundSpecifier SparkSound = new SoundCollectionSpecifier("sparks");

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public bool CanSmesEtc = false;
    #region FixEnergyMinus
    public bool BuyedOverload = false;
    public bool BuyedDash = false;
    public bool BuyedEMP = false;
    public bool BuyedHeal = false;
    public bool BuyedShield = false;
    public bool BuyedSpeed = false;
    public bool BuyedLightning = false;
    public bool BuyedDouble = false;
    public bool BuyedUpgrade = false;
    #endregion
}
