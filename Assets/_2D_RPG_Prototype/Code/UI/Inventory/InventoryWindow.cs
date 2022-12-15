using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.Inventory
{
    public class InventoryWindow : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _itemInfoText;
        [SerializeField] private TextMeshProUGUI _useButtonText;

        [Header("Buttons")]
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _discardButton;

        [Header("Others")]
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _choiceButtonsContainer;
        [SerializeField] private GameObject _actionPanel;
        [SerializeField] private GameObject _choicePanel;

        [Header("Prefabs")]
        [SerializeField] private InventoryButton _buttonPrefab;
        [SerializeField] private ChoiceButton _choiceButtonPrefab;

        public InventoryButton SelectedButton => _buttons.FirstOrDefault(x => x.Contains(_selectedItem));

        private const string DEFAULT_BUTTON_LABLE = "Consume";
        private const string EQUIP_BUTTON_LABLE = "Equip";

        private List<InventoryButton> _buttons;
        private List<ChoiceButton> _choiceButtons;
        private IInventoryService _inventory;
        private InventoryItem[] _items;
        private InventoryItem _selectedItem;

        private void Awake()
        {
            var existingButtons = _container.GetComponentsInChildren<InventoryButton>(true);
            _buttons = new List<InventoryButton>(existingButtons);

            foreach (var button in _buttons)
                button.onItemSelect += OnItemSelected;

            _inventory = ServiceProvider.GetService<IInventoryService>();

            _useButton.onClick.AddListener(ShowChoicePanel);
            _discardButton.onClick.AddListener(DiscardSelectedItem);

            InitializeChoiceButtons();
        }

        private void OnEnable()
        {
            ResetPanels();
            RefreshButtons();

            _itemInfoText.SetText("Pick an item");
        }

        private void OnDestroy()
        {
            _useButton.onClick.RemoveListener(ShowChoicePanel);
            _discardButton.onClick.RemoveListener(DiscardSelectedItem);

            foreach (var button in _buttons)
                button.onItemSelect -= OnItemSelected;

            foreach (var button in _choiceButtons)
                button.onCharacterSelect -= OnCharacterSelected;
        }

        private void InitializeChoiceButtons()
        {
            _choiceButtons = new List<ChoiceButton>();

            var characters = ServiceProvider.GetService<IStatsManager>().Stats;
            foreach (var character in characters)
            {
                var button = Instantiate(_choiceButtonPrefab, _choiceButtonsContainer);
                button.Initialize(character);
                button.onCharacterSelect += OnCharacterSelected;
                _choiceButtons.Add(button);
            }
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
            _actionPanel.SetActive(true);
            _choicePanel.SetActive(false);

            SelectItem(item);
        }

        private void OnCharacterSelected(CharacterStats character)
        {
            _choicePanel.SetActive(false);

            if (_selectedItem is not ICharacterStatsApplicable applicable)
                return;

            applicable.Apply(character);
            _inventory.Remove(_selectedItem);

            int remainingAmount = _inventory.Count(_selectedItem);
            if (remainingAmount <= 0)
            {
                RemoveSelectedItemButton();
                return;
            }

            SelectedButton.RefreshCounter(remainingAmount);
        }

        private void SelectItem(InventoryItem item)
        {
            _selectedItem = item;
            _itemInfoText.SetText($"{item.Name} - {item.GetDescription()}");
            _useButtonText.SetText(item is EquipableInventoryItem ? EQUIP_BUTTON_LABLE : DEFAULT_BUTTON_LABLE);
            _useButton.interactable = item is ICharacterStatsApplicable;
        }

        private void RefreshButtons()
        {
            if (_buttons.Count != _inventory.Count())
                RefreshButtonsAmount();

            _items = _inventory.Items;

            for (int i = 0; i < _items.Length; i++)
                _buttons[i].Initialize(_items[i], _inventory.Count(_items[i]));
        }

        private void ShowChoicePanel() =>
            _choicePanel.SetActive(true);

        private void DiscardSelectedItem()
        {
            int count = _inventory.Count(_selectedItem);
            _inventory.Remove(_selectedItem, count);

            RemoveSelectedItemButton();
        }

        private void RemoveSelectedItemButton()
        {
            var button = SelectedButton;
            _buttons.Remove(button);
            Destroy(button.gameObject);

            ResetPanels();
        }

        private void ResetPanels()
        {
            _selectedItem = null;
            _actionPanel.SetActive(false);
            _choicePanel.SetActive(false);
        }
    }
}
