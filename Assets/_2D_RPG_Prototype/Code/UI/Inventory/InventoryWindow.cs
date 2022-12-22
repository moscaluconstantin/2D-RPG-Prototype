using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.Inventory
{
    public class InventoryWindow : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _useButtonText;

        [Header("Buttons")]
        [SerializeField] private Button _useButton;
        [SerializeField] private Button _discardButton;

        [Header("Prefabs")]
        [SerializeField] private ChoiceButton _choiceButtonPrefab;

        [Header("Components")]
        [SerializeField] private InventoryItemsViewer _itemsViewer;

        [Header("Others")]
        [SerializeField] private Transform _choiceButtonsContainer;
        [SerializeField] private GameObject _actionPanel;
        [SerializeField] private GameObject _choicePanel;

        private const string DEFAULT_BUTTON_LABLE = "Consume";
        private const string EQUIP_BUTTON_LABLE = "Equip";

        private List<ChoiceButton> _choiceButtons;
        private IInventoryService _inventory;
        private InventoryItem _selectedItem;

        private void Awake()
        {
            _inventory = ServiceProvider.GetService<IInventoryService>();
            _itemsViewer.Initialize(_inventory);

            _useButton.onClick.AddListener(ShowChoicePanel);
            _discardButton.onClick.AddListener(DiscardSelectedItem);
            _itemsViewer.itemSelected += OnItemSelected;
            _itemsViewer.selectedItemRemoved += ResetPanels;

            InitializeChoiceButtons();
        }

        private void OnEnable() =>
            ResetPanels();

        private void OnDestroy()
        {
            _useButton.onClick.RemoveListener(ShowChoicePanel);
            _discardButton.onClick.RemoveListener(DiscardSelectedItem);
            _itemsViewer.itemSelected -= OnItemSelected;
            _itemsViewer.selectedItemRemoved -= ResetPanels;

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
        }

        private void SelectItem(InventoryItem item)
        {
            _selectedItem = item;
            _useButtonText.SetText(item is EquipableInventoryItem ? EQUIP_BUTTON_LABLE : DEFAULT_BUTTON_LABLE);
            _useButton.interactable = item is ICharacterStatsApplicable;
        }

        private void ShowChoicePanel() =>
            _choicePanel.SetActive(true);

        private void DiscardSelectedItem()
        {
            int count = _inventory.Count(_selectedItem);
            _inventory.Remove(_selectedItem, count);
        }

        private void ResetPanels()
        {
            _selectedItem = null;
            _actionPanel.SetActive(false);
            _choicePanel.SetActive(false);
        }
    }
}
