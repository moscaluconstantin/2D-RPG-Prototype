using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Enums;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems
{
    [CreateAssetMenu(fileName = ResourceFileNames.INVENTORY_ITEM, menuName = ResourcesMenu.INVENTORY_ITEMS + "EquipableInventoryItem", order = 1)]
    public class EquipableInventoryItem : WithStatsInventoryItem
    {
        [SerializeField] private EquipmentSlotType _slotType;

        public EquipmentSlotType SlotType => _slotType;
    }
}
