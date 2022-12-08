using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.Stats
{
    public class StatsButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        public event Action<CharacterStats> OnClicked;

        private CharacterStats _stats;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnClick);

        public void Initialize(CharacterStats stats)
        {
            _stats = stats;
            _label.SetText(_stats.Name);
        }

        private void OnClick() =>
            OnClicked?.Invoke(_stats);
    }
}
