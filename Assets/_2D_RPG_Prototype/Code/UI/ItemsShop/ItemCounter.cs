using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.ItemsShop
{
    public class ItemCounter : MonoBehaviour
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _counterText;

        private IInventoryService _inventory;
        private bool _isInitialized = false;

        private void OnEnable()
        {
            if (!_isInitialized)
                return;

            Activate();
        }

        private void OnDisable()
        {
            if (!_isInitialized)
                return;

            Deactivate();
        }

        public void Initialize(IInventoryService inventory)
        {
            if (_isInitialized)
                Deactivate();

            _isInitialized = true;
            _inventory = inventory;

            Activate();
        }

        private void Activate()
        {
            UpdateCounter();

            _inventory.OnItemsAmountChanged += UpdateCounter;
            _inventory.OnItemsCountChanged += UpdateCounter;
        }

        private void Deactivate()
        {
            _inventory.OnItemsAmountChanged -= UpdateCounter;
            _inventory.OnItemsCountChanged -= UpdateCounter;
        }

        private void UpdateCounter() =>
            _counterText.SetText(_inventory.Count(_item).ToString());

        [ContextMenu("Update Image")]
        private void UpdateImage()
        {
            _image.sprite = _item.Image;
        }
    }
}
