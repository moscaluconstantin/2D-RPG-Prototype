using System;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public class InventorySaveDataSlot
    {
        public int ItemId;
        public int Count;

        public InventorySaveDataSlot(int itemId, int count)
        {
            ItemId = itemId;
            Count = count;
        }
    }
}
