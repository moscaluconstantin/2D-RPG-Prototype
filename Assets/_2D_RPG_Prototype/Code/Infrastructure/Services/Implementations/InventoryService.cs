using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class InventoryService : MonoBehaviour, IInventoryService
    {
        [SerializeField] private List<InventorySlot> _slots;

        public InventoryItem[] Items => _slots.Select(x => x.Item).ToArray();

        public int Count() =>
            _slots.Count;

        public int Count(InventoryItem item)
        {
            if (_slots.Any(x => x.Item.Id == item.Id))
                return _slots.First(x => x.Item.Id == item.Id).Count;

            return 0;
        }

        public bool Contains(InventoryItem item, int amount = 1) =>
            Count(item) >= amount;

        public void Add(InventoryItem item, int amount = 1)
        {
            if (_slots.Any(x => x.Item.Id == item.Id))
            {
                _slots.First(x => x.Item.Id == item.Id).Count += amount;
                return;
            }

            _slots.Add(new InventorySlot()
            {
                Item = item,
                Count = amount
            });
        }

        public void Remove(InventoryItem item, int amount = 1)
        {
            if (_slots.Any(x => x.Item.Id == item.Id))
            {
                var slot = _slots.First(x => x.Item.Id == item.Id);

                if (slot.Count > amount)
                {
                    slot.Count -= amount;
                    return;
                }

                _slots.Remove(slot);
            }
        }
    }
}
