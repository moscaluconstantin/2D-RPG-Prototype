using Assets._2D_RPG_Prototype.Code.Enums;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI
{
    public class EquipmentSlot : MonoBehaviour
    {
        [SerializeField] private EquipmentSlotType _slotType;
        [SerializeField] private Sprite _defaultIcon;

        [Header("Components")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        public event Action<EquipmentSlotType> OnClicked;

        public EquipmentSlotType SlotType => _slotType;

        private void Awake() =>
            _button.onClick.AddListener(OnClick);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnClick);

        public void SetIcon(Sprite icon) =>
            _image.sprite = icon;

        public void SetDefaultIcon() =>
            SetIcon(_defaultIcon);

        private void OnClick()
        {
            SetDefaultIcon();
            OnClicked?.Invoke(_slotType);
        }
    }
}
