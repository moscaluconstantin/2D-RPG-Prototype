using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.Inventory
{
    public class InventoryItemsViewer : MonoBehaviour
    {
        [SerializeField] private InventoryButton _buttonPrefab;
        [SerializeField] private TextMeshProUGUI _itemInfoText;
        [SerializeField] private Transform _container;

        public event Action<InventoryItem> itemSelected;
        public event Action selectedItemRemoved;

        private IInventoryService _inventory;
        private List<InventoryButton> _buttons;
        private InventoryItem[] _items;
        private bool _isInitialized = false;
        private InventoryItem _lastSelected = null;

        private void OnEnable() =>
            ForgetSelected();

        private void OnDestroy()
        {
            if (_inventory != null)
            {
                _inventory.OnItemsAmountChanged -= RefreshButtons;
                _inventory.OnItemsAmountChanged -= CheckSelectedItem;
                _inventory.OnItemsCountChanged -= RefreshCounters;
            }

            foreach (var button in _buttons)
                button.onItemSelect -= OnItemSelected;
        }

        public void Initialize(IInventoryService inventory)
        {
            if (_isInitialized)
                return;

            _inventory = inventory;

            _inventory.OnItemsAmountChanged += RefreshButtons;
            _inventory.OnItemsAmountChanged += CheckSelectedItem;
            _inventory.OnItemsCountChanged += RefreshCounters;

            InitializeButtons();
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
            if (_buttons.Count != _inventory.Count())
                RefreshButtonsAmount();

            _items = _inventory.Items;

            for (int i = 0; i < _items.Length; i++)
                _buttons[i].Initialize(_items[i], _inventory.Count(_items[i]));
        }

        private void RefreshButtonsAmount()
        {
            int diff = _inventory.Count() - _buttons.Count;

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
            _itemInfoText.SetText($"{item.Name} - {item.GetDescription()}");
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

            ForgetSelected();

            selectedItemRemoved?.Invoke();
        }

        private void ForgetSelected()
        {
            _lastSelected = null;
            _itemInfoText.SetText("Pick an item");
        }
    }
}
