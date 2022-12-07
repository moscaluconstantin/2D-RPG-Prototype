using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._2D_RPG_Prototype.Code.UI.Stats
{
    public class CharacterStatsView : MonoBehaviour
    {
        [SerializeField] private Image _avatarImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _healthValueText;
        [SerializeField] private TextMeshProUGUI _manaValueText;
        [SerializeField] private TextMeshProUGUI _experienceValueText;
        [SerializeField] private Slider _experienceSlider;

        private bool IsInitialized => _stats != null;

        private CharacterStats _stats;

        public void Initialize(CharacterStats stats)
        {
            _stats = stats;

            Refresh();
        }

        public void Refresh()
        {
            if (!IsInitialized)
                return;

            _avatarImage.sprite = _stats.Image;
            _nameText.SetText(_stats.name);
            _levelText.SetText(_stats.Level.ToString());
            _healthValueText.SetText($"{_stats.Health}/{_stats.MaxHealth}");
            _manaValueText.SetText($"{_stats.Mana}/{_stats.MaxMana}");
            _experienceValueText.SetText($"{_stats.Experience}/{_stats.MaxExperience}");
            _experienceSlider.value = (float)_stats.Experience / _stats.MaxExperience;
        }
    }
}
