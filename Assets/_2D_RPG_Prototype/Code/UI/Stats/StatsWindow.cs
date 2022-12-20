using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using Assets._2D_RPG_Prototype.Code.UI.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsWindow : MonoBehaviour
{
    [Header("Character info")]
    [SerializeField] private Image _avatarImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _damageValueText;
    [SerializeField] private TextMeshProUGUI _defenceValueText;
    [SerializeField] private TextMeshProUGUI _healthValueText;
    [SerializeField] private TextMeshProUGUI _manaValueText;
    [SerializeField] private TextMeshProUGUI _experienceValueText;

    [Header("Other components")]
    [SerializeField] private Transform _buttonsContainer;
    [SerializeField] private StatsButton _statsButtonPrefab;

    private StatsButton[] _buttons;
    private CharacterStats[] _stats;
    private CharacterStats _selectedStats;

    private void Awake()
    {
        _stats = ServiceProvider.GetService<IStatsManager>().Stats;
        _selectedStats = _stats[0];

        InitButtons();
        Refresh();
    }

    private void OnEnable() =>
        Refresh();

    private void InitButtons()
    {
        var existingButtons = _buttonsContainer.GetComponentsInChildren<StatsButton>();
        foreach (var button in existingButtons)
            Destroy(button.gameObject);

        _buttons = new StatsButton[_stats.Length];
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i] = Instantiate(_statsButtonPrefab, _buttonsContainer);
            _buttons[i].Initialize(_stats[i]);
            _buttons[i].OnClicked += SelectStats;
        }
    }

    private void SelectStats(CharacterStats characterStats)
    {
        _selectedStats = characterStats;
        Refresh();
    }

    private void Refresh()
    {
        _avatarImage.sprite = _selectedStats.Image;
        _nameText.SetText(_selectedStats.Name);
        _levelText.SetText(_selectedStats.Level.ToString());
        //_strengthValueText.SetText()
        _defenceValueText.SetText(_selectedStats.Defence.ToString());
        _healthValueText.SetText($"{_selectedStats.Health}/{_selectedStats.MaxHealth}");
        _manaValueText.SetText($"{_selectedStats.Mana}/{_selectedStats.MaxMana}");
        _experienceValueText.SetText($"{_selectedStats.Experience}/{_selectedStats.MaxExperience}");
    }
}
