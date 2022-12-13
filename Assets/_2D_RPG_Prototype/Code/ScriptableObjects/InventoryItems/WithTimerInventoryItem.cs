using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems
{
    [CreateAssetMenu(fileName = ResourceFileNames.INVENTORY_ITEM, menuName = ResourcesMenu.INVENTORY_ITEMS + "WithTimerInventoryItem", order = 1)]
    public class WithTimerInventoryItem : InventoryItem
    {
        [SerializeField] private float _duration;
        [SerializeField] private StatValue[] _stats;

        public float Duration => _duration;
        public StatValue[] Stats => _stats;
    }
}
