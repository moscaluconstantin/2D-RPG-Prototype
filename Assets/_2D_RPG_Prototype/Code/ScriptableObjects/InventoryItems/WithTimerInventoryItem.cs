using Assets._2D_RPG_Prototype.Code.Constants;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems
{
    [CreateAssetMenu(fileName = ResourceFileNames.INVENTORY_ITEM, menuName = ResourcesMenu.INVENTORY_ITEMS + "WithTimerInventoryItem", order = 1)]
    public class WithTimerInventoryItem : WithStatsInventoryItem, ICharacterStatsApplicable
    {
        [SerializeField] private float _duration;

        public float Duration => _duration;

        public override string GetDescription()
        {
            string description = base.GetDescription();
            description.Replace("_d", _duration.ToString());

            return description;
        }

        public void Apply(CharacterStats characterStats)
        {
            Debug.Log($"Apply {Name} to {characterStats.Name}");
        }
    }
}
