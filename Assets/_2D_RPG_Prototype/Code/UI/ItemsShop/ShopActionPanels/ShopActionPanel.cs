using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.NPC;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using Assets._2D_RPG_Prototype.Code.UI.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.ItemsShop.ShopActionPanels
{
    public abstract class ShopActionPanel : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _itemPriceText;
        [SerializeField] GameObject _container;

        protected InventoryItemsViewer ItemsViewer;
        protected IInventoryService PlayerInventory;
        protected Shopkeeper Shopkeeper;

        private bool _isInitialized = false;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClicked);

            if (_isInitialized)
                Deactivate();
        }

        public void Initialize(InventoryItemsViewer itemsViewer, IInventoryService playerInventory)
        {
            ItemsViewer = itemsViewer;
            PlayerInventory = playerInventory;

            _isInitialized = true;
        }

        public void Activate(Shopkeeper shopkeeper)
        {
            if (!_isInitialized)
                return;

            Shopkeeper = shopkeeper;

            ItemsViewer.itemSelected += OnitemSelected;
            ItemsViewer.selectedItemRemoved += Hide;
        }

        public void Deactivate()
        {
            Hide();

            ItemsViewer.itemSelected -= OnitemSelected;
            ItemsViewer.selectedItemRemoved -= Hide;
        }

        protected abstract void OnButtonClicked();

        protected virtual void OnitemSelected(InventoryItem item) =>
            Show();

        protected void SetPriceText(string text) =>
            _itemPriceText.SetText(text);

        protected void SetButtonState(bool state) =>
            _button.interactable = state;

        private void Show() =>
            _container.SetActive(true);

        private void Hide() =>
            _container.SetActive(false);
    }
}
