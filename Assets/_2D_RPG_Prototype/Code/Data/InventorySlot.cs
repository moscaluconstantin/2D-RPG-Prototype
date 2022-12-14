using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public class InventorySlot
    {
        public InventoryItem Item;
        public int Count;
    }
}
