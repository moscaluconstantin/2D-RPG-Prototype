using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems
{
    [CreateAssetMenu(fileName = ResourceFileNames.INVENTORY_ITEM, menuName = ResourcesMenu.INVENTORY_ITEMS + "ConsumableInventoryItem", order = 1)]
    public class ConsumableInventoryItem : InventoryItem
    {
        [SerializeField] private StatValue[] _stats;

        public StatValue[] Stats => _stats;
    }
}
