using Assets._2D_RPG_Prototype.Code.Data;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems
{
    public abstract class WithStatsInventoryItem : InventoryItem
    {
        [SerializeField] private StatValue[] _stats;

        public StatValue[] Stats => _stats;

        public override string GetDescription()
        {
            string description = base.GetDescription();

            for (int i = 0; i < _stats.Length; i++)
            {
                //bool contains = description.Contains($"_v{i}");
                description = description.Replace($"_v{i}", _stats[i].Value.ToString());
            }

            return description;
        }
    }
}
