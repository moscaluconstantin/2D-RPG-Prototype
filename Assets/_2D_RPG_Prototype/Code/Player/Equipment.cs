using Assets._2D_RPG_Prototype.Code.Enums;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System.Collections.Generic;
using System.Linq;

namespace Assets._2D_RPG_Prototype.Code.Player
{
    public class Equipment
    {
        private List<EquipableInventoryItem> _items;
        private Dictionary<StatType, int> _stats;
        private IInventoryService _inventory;

        public List<EquipableInventoryItem> Items => _items;

        public Equipment(IInventoryService inventory)
        {
            _items = new List<EquipableInventoryItem>();
            _stats = new Dictionary<StatType, int>();
            _inventory = inventory;
        }

        public void Add(EquipableInventoryItem item)
        {
            Clear(item.SlotType);
            Equip(item);
        }

        public void Clear(EquipmentSlotType slotType)
        {
            var equipedItem = _items.FirstOrDefault(x => x.SlotType == slotType);

            if (equipedItem == null)
                return;

            Unequip(equipedItem);
            _inventory.Add(equipedItem);
        }

        public int GetStatValue(StatType statType) =>
            _stats.TryGetValue(statType, out int value) ? value : 0;

        private void Equip(EquipableInventoryItem item)
        {
            _items.Add(item);

            foreach (var stat in item.Stats)
            {
                if (_stats.ContainsKey(stat.Type))
                {
                    _stats[stat.Type] += stat.Value;
                }
                else
                {
                    _stats.Add(stat.Type, stat.Value);
                }
            }
        }

        private void Unequip(EquipableInventoryItem item)
        {
            _items.Remove(item);

            foreach (var stat in item.Stats)
                _stats[stat.Type] -= stat.Value;
        }
    }
}
