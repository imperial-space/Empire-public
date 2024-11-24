using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared.PrinterDoc
{
    [Serializable, NetSerializable]
    public sealed class PrinterDocCheckIdCardMessage : BoundUserInterfaceMessage
    {
        public bool UseCardId { get; }
        public PrinterDocCheckIdCardMessage(bool useCardId)
        {
            UseCardId = useCardId;
        }
    }

    [Serializable, NetSerializable]
    public sealed class PrinterDocHelpers
    {
        public static bool TryToCheckPrinterShared(EntityUid uid, IEntityManager entityManager, IPrototypeManager prototypeManager)
        {
            if (entityManager.TryGetComponent(uid, out MetaDataComponent? metaData))
            {
                var prototypeId = metaData.EntityPrototype?.ID;
                if (prototypeId != null && prototypeManager.TryIndex<EntityPrototype>(prototypeId, out var prototype))
                {
                    if (prototype.ID == "PrinterDoc")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
