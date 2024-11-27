using Robust.Shared.Serialization;

namespace Content.Shared.Imperial.PlantsAnalyzer
{
    [Serializable, NetSerializable]
    public sealed class PlantsAnalyzerScannedUserMessage : BoundUserInterfaceMessage
    {
        public readonly NetEntity? TargetEntity;
        public readonly string PlantName;
        public readonly float PotencyLevel;
        public readonly float ProductionLevel;
        public readonly float PestLevel;
        public readonly float WeedLevel;
        public readonly float Toxins;
        public readonly int Age;
        public readonly float Health;
        public readonly float MutationLevel;
        public readonly bool ImproperHeat;
        public readonly bool ImproperPressure;
        public readonly bool ImproperLight;
        public readonly bool IsDead;
        public readonly bool ScanMode;
        public readonly string OptimalConditions;
        public readonly bool HasKudzu;
        public readonly string Mutations;
        public readonly string Chemicals;

        public PlantsAnalyzerScannedUserMessage(
            NetEntity? targetEntity,
            string plantName,
            float potencyLevel,
            float productionLevel,
            float pestLevel,
            float weedLevel,
            float toxins,
            int age,
            float health,
            float mutationLevel,
            bool improperHeat,
            bool improperPressure,
            bool improperLight,
            bool isDead,
            bool scanMode,
            string optimalConditions,
            bool hasKudzu,
            string mutations,
            string chemicals)
        {
            TargetEntity = targetEntity;
            PlantName = plantName;
            PotencyLevel = potencyLevel;
            ProductionLevel = productionLevel;
            PestLevel = pestLevel;
            WeedLevel = weedLevel;
            Toxins = toxins;
            Age = age;
            Health = health;
            MutationLevel = mutationLevel;
            ImproperHeat = improperHeat;
            ImproperPressure = improperPressure;
            ImproperLight = improperLight;
            IsDead = isDead;
            ScanMode = scanMode;
            OptimalConditions = optimalConditions;
            HasKudzu = hasKudzu;
            Mutations = mutations;
            Chemicals = chemicals;
        }
    }
}
