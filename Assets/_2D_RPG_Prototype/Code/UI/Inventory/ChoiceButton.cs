using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.Inventory
{
    public class ChoiceButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _characterNameText;
        [SerializeField] private Button _button;

        public event Action<CharacterStats> onCharacterSelect;

        private CharacterStats _character;

        private void Awake() =>
            _button.onClick.AddListener(OnClicked);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnClicked);

        public void Initialize(CharacterStats character)
        {
            _character = character;
            _characterNameText.SetText(character.Name);
        }

        private void OnClicked() =>
            onCharacterSelect?.Invoke(_character);
    }
}
