using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class InventoryService : MonoBehaviour, IInventoryService
    {
        [SerializeField] private string _saveKey = "unknown";
        [SerializeField] private List<InventorySlot> _slots;

        public event Action OnItemsAmountChanged;
        public event Action OnItemsCountChanged;

        public InventoryItem[] Items => _slots.Select(x => x.Item).ToArray();
        private string ItemsSaveKey => $"{_saveKey}_{SaveKeys.INVENTORY_ITEMS}";
        private string CountersSaveKey => $"{_saveKey}_{SaveKeys.INVENTORY_COUNTERS}";

        private void Awake() =>
            SortItems();

        public int Count() =>
            _slots.Count;

        public int Count(InventoryItem item)
        {
            if (item != null && _slots.Any(x => x.Item.Id == item.Id))
                return _slots.First(x => x.Item.Id == item.Id).Count;

            return 0;
        }

        public bool Contains(InventoryItem item, int amount = 1) =>
            Count(item) >= amount;

        public bool Contains(Price[] priceList)
        {
            foreach (var price in priceList)
            {
                if (!Contains(price.Item, price.Count))
                    return false;
            }

            return true;
        }

        public void Add(InventoryItem item, int amount = 1)
        {
            if (_slots.Any(x => x.Item.Id == item.Id))
            {
                _slots.First(x => x.Item.Id == item.Id).Count += amount;
                OnItemsCountChanged?.Invoke();
                return;
            }

            _slots.Add(new InventorySlot()
            {
                Item = item,
                Count = amount
            });
            SortItems();

            OnItemsAmountChanged?.Invoke();
        }

        public void Add(Price[] priceList)
        {
            foreach (var price in priceList)
                Add(price.Item, price.Count);
        }

        public void Remove(InventoryItem item, int amount = 1)
        {
            if (_slots.Any(x => x.Item.Id == item.Id))
            {
                var slot = _slots.First(x => x.Item.Id == item.Id);

                if (slot.Count > amount)
                {
                    slot.Count -= amount;
                    OnItemsCountChanged?.Invoke();
                    return;
                }

                _slots.Remove(slot);
                OnItemsAmountChanged?.Invoke();
            }
        }

        public void Remove(Price[] priceList)
        {
            foreach (var price in priceList)
                Remove(price.Item, price.Count);
        }

        public void Clear() =>
            _slots.Clear();

        private void SortItems() =>
            _slots = _slots.OrderBy(x => x.Item.Id).ToList();

        public void Save()
        {
            var items = _slots.Select(x => x.Item.Id).ToArray();
            var counters = _slots.Select(x => x.Count).ToArray();

            SaveLoadService.Save(ItemsSaveKey, items);
            SaveLoadService.Save(CountersSaveKey, counters);
        }

        public void Load()
        {
            if (!PlayerPrefs.HasKey(ItemsSaveKey) || !PlayerPrefs.HasKey(CountersSaveKey))
                return;

            Clear();

            var resourcesDatabase = ServiceProvider.GetService<IResourcesDatabase>();
            var items = SaveLoadService.Load(ItemsSaveKey, new int[0]);
            var counters = SaveLoadService.Load(CountersSaveKey, new int[0]);

            for (int i = 0; i < items.Length; i++)
            {
                var item = resourcesDatabase.GetInventoryItem(items[i]);
                Add(item, counters[i]);
            }
        }
    }
}
