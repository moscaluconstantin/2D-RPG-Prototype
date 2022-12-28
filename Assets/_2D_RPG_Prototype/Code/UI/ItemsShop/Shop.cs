using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.NPC;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using Assets._2D_RPG_Prototype.Code.UI.Inventory;
using Assets._2D_RPG_Prototype.Code.UI.ItemsShop.ShopActionPanels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.ItemsShop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _itemInfoText;

        [Header("Buttons")]
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Button _closeButton;

        [Header("Components")]
        [SerializeField] private InventoryItemsViewer _itemsViewer;
        [SerializeField] private ShopActionPanel _buyActionPanel;
        [SerializeField] private ShopActionPanel _sellActionPanel;
        [SerializeField] private MoneyPanel _shopMoneyPanel;
        [SerializeField] private MoneyPanel _playerMoneyPanel;

        private IInventoryService _playerInventory;
        private Shopkeeper _shopkeeper;

        private void Awake()
        {
            _playerInventory = ServiceProvider.GetService<IInventoryService>();
            _playerMoneyPanel.Initialize(_playerInventory);

            _buyButton.onClick.AddListener(OpenBuyWindow);
            _sellButton.onClick.AddListener(OpenSellWindow);
            _closeButton.onClick.AddListener(Hide);
            _itemsViewer.itemSelected += OnitemSelected;
            _itemsViewer.selectedItemRemoved += ResetItemInfoText;

            InitActionPanels();
        }

        private void Start() =>
            Hide();

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(OpenBuyWindow);
            _sellButton.onClick.RemoveListener(OpenSellWindow);
            _closeButton.onClick.RemoveListener(Hide);
            _itemsViewer.itemSelected -= OnitemSelected;
            _itemsViewer.selectedItemRemoved -= ResetItemInfoText;
        }

        public void Show(Shopkeeper shopkeeper)
        {
            _shopkeeper = shopkeeper;

            gameObject.SetActive(true);
            _shopMoneyPanel.Initialize(shopkeeper.Inventory);
            ResetItemInfoText();
            OpenBuyWindow();
        }

        private void InitActionPanels()
        {
            _buyActionPanel.Initialize(_itemsViewer, _playerInventory);
            _buyActionPanel.Deactivate();

            _sellActionPanel.Initialize(_itemsViewer, _playerInventory);
            _sellActionPanel.Deactivate();
        }

        private void Hide()
        {
            _buyActionPanel.Deactivate();
            _sellActionPanel.Deactivate();

            gameObject.SetActive(false);
        }

        private void OpenBuyWindow()
        {
            ResetItemInfoText();
            _itemsViewer.Initialize(_shopkeeper.Inventory, _shopkeeper.ForBuyingIds);
            _sellActionPanel.Deactivate();
            _buyActionPanel.Activate(_shopkeeper);

            _buyButton.interactable = false;
            _sellButton.interactable = true;
        }

        private void OpenSellWindow()
        {
            ResetItemInfoText();
            _itemsViewer.Initialize(_playerInventory, _shopkeeper.ForBuyingIds);
            _buyActionPanel.Deactivate();
            _sellActionPanel.Activate(_shopkeeper);

            _buyButton.interactable = true;
            _sellButton.interactable = false;
        }

        private void OnitemSelected(InventoryItem item) =>
            _itemInfoText.SetText($"{item.Name} - {item.GetDescription()}");

        private void ResetItemInfoText() =>
            _itemInfoText.SetText("Pick an item");
    }
}
