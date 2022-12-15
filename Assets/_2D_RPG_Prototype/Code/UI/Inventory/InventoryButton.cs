using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.Inventory
{
    public class InventoryButton : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Button _button;

        public event Action<InventoryItem> onItemSelect;

        private InventoryItem _item;

        private void Awake() =>
            _button.onClick.AddListener(OnClicked);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnClicked);

        public void Initialize(InventoryItem item, int amount)
        {
            _item = item;
            _icon.sprite = item.Image;

            RefreshCounter(amount);
        }

        public void RefreshCounter(int amount) =>
            _countText.SetText(amount.ToString());

        public bool Contains(InventoryItem item) =>
            _item != null && _item.Id == item.Id;

        private void OnClicked() =>
            onItemSelect?.Invoke(_item);
    }
}
