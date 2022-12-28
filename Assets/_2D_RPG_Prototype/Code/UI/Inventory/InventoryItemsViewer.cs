using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.Inventory
{
    public class InventoryItemsViewer : MonoBehaviour
    {
        [SerializeField] private InventoryButton _buttonPrefab;
        [SerializeField] private Transform _container;

        public event Action<InventoryItem> itemSelected;
        public event Action selectedItemRemoved;

        public InventoryItem LastSelected => _lastSelected;

        private IInventoryService _inventory;
        private List<InventoryButton> _buttons;
        private List<int> _filter;
        private InventoryItem[] _items;
        private InventoryItem _lastSelected = null;
        private bool _isInitialized = false;
        private bool _filterItems = false;

        private void OnEnable()
        {
            _filter = null;
            _filterItems = false;
            _lastSelected = null;
        }

        private void OnDestroy()
        {
            if (_isInitialized)
                UnsubscribeFromInventoryActions();

            //foreach (var button in _buttons)
            //    button.onItemSelect -= OnItemSelected;
        }

        public void Initialize(IInventoryService inventory)
        {
            if (_isInitialized)
                UnsubscribeFromInventoryActions();

            _inventory = inventory;

            if (_isInitialized)
                RefreshButtons();
            else
                InitializeButtons();

            _isInitialized = true;

            _inventory.OnItemsAmountChanged += RefreshButtons;
            _inventory.OnItemsAmountChanged += CheckSelectedItem;
            _inventory.OnItemsCountChanged += RefreshCounters;
        }

        public void Initialize(IInventoryService inventory, List<int> filter)
        {
            _filter = filter;
            _filterItems = true;

            Initialize(inventory);
        }

        private void UnsubscribeFromInventoryActions()
        {
            _inventory.OnItemsAmountChanged -= RefreshButtons;
            _inventory.OnItemsAmountChanged -= CheckSelectedItem;
            _inventory.OnItemsCountChanged -= RefreshCounters;
        }

        private void InitializeButtons()
        {
            var existingButtons = _container.GetComponentsInChildren<InventoryButton>(true);
            _buttons = new List<InventoryButton>(existingButtons);

            foreach (var button in _buttons)
                button.onItemSelect += OnItemSelected;

            RefreshButtons();
        }

        private void RefreshButtons()
        {
            _items = _inventory.Items;

            if (_filterItems)
                _items = _items.Where(x => _filter.Contains(x.Id)).ToArray();

            if (_buttons.Count != _items.Length)
                RefreshButtonsAmount();

            for (int i = 0; i < _items.Length; i++)
                _buttons[i].Initialize(_items[i], _inventory.Count(_items[i]));
        }

        private void RefreshButtonsAmount()
        {
            int diff = _items.Length - _buttons.Count;

            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                {
                    var button = Instantiate(_buttonPrefab, _container);
                    button.onItemSelect += OnItemSelected;
                    _buttons.Add(button);
                }
            }
            else if (diff < 0)
            {
                var toRemove = _buttons.Take(Mathf.Abs(diff)).ToList();


                foreach (var button in toRemove)
                {
                    button.onItemSelect -= OnItemSelected;
                    _buttons.Remove(button);
                    Destroy(button.gameObject);
                }
            }
        }

        private void OnItemSelected(InventoryItem item)
        {
            _lastSelected = item;
            itemSelected?.Invoke(item);
        }

        private void RefreshCounters()
        {
            for (int i = 0; i < _items.Length; i++)
                _buttons[i].RefreshCounter(_inventory.Count(_items[i]));
        }

        private void CheckSelectedItem()
        {
            if (_inventory.Count(_lastSelected) > 0)
                return;

            _lastSelected = null;

            selectedItemRemoved?.Invoke();
        }
    }
}
