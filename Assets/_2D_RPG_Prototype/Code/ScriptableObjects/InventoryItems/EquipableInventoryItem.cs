using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Enums;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems
{
    [CreateAssetMenu(fileName = ResourceFileNames.INVENTORY_ITEM, menuName = ResourcesMenu.INVENTORY_ITEMS + "EquipableInventoryItem", order = 1)]
    public class EquipableInventoryItem : WithStatsInventoryItem, ICharacterStatsApplicable
    {
        [SerializeField] private EquipmentSlotType _slotType;

        public EquipmentSlotType SlotType => _slotType;

        public void Apply(CharacterStats characterStats)
        {
            Debug.Log($"Apply {Name} to {characterStats.Name}");
        }
    }
}
