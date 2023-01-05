using System;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public class InventorySaveData
    {
        public InventorySaveDataSlot[] Slots;

        public InventorySaveData() =>
            Slots = new InventorySaveDataSlot[0];

        public InventorySaveData(InventorySaveDataSlot[] slots) =>
            Slots = slots;
    }
}
