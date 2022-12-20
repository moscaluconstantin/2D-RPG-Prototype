using Assets._2D_RPG_Prototype.Code.Enums;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using Assets._2D_RPG_Prototype.Code.UI;
using Assets._2D_RPG_Prototype.Code.UI.Stats;
using System.Linq;
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
    [SerializeField] private Transform _slotsContainer;
    [SerializeField] private StatsButton _statsButtonPrefab;

    private StatsButton[] _characterButtons;
    private EquipmentSlot[] _slots;
    private IInventoryService _inventory;
    private CharacterStats[] _stats;
    private CharacterStats _selectedStats;

    private void Awake()
    {
        _inventory = ServiceProvider.GetService<IInventoryService>();
        _stats = ServiceProvider.GetService<IStatsManager>().Stats;
        _selectedStats = _stats[0];

        InitCharacterButtons();
        InitSlots();
        Refresh();
    }

    private void OnEnable() =>
        Refresh();

    private void OnDestroy()
    {
        foreach (var button in _characterButtons)
            button.OnClicked -= SelectStats;

        foreach (var slot in _slots)
            slot.OnClicked -= Unequip;
    }

    private void InitCharacterButtons()
    {
        var existingButtons = _buttonsContainer.GetComponentsInChildren<StatsButton>();
        foreach (var button in existingButtons)
            Destroy(button.gameObject);

        _characterButtons = new StatsButton[_stats.Length];
        for (int i = 0; i < _characterButtons.Length; i++)
        {
            _characterButtons[i] = Instantiate(_statsButtonPrefab, _buttonsContainer);
            _characterButtons[i].Initialize(_stats[i]);
            _characterButtons[i].OnClicked += SelectStats;
        }
    }

    private void InitSlots()
    {
        _slots = _slotsContainer.GetComponentsInChildren<EquipmentSlot>();
        foreach (var slot in _slots)
            slot.OnClicked += Unequip;
    }

    private void SelectStats(CharacterStats characterStats)
    {
        _selectedStats = characterStats;
        Refresh();
    }

    private void Unequip(EquipmentSlotType slotType)
    {
        _selectedStats.Equipment.Clear(slotType);
        RefreshStats();
    }

    private void Refresh()
    {
        _avatarImage.sprite = _selectedStats.Image;
        _nameText.SetText(_selectedStats.Name);
        _levelText.SetText(_selectedStats.Level.ToString());
        _experienceValueText.SetText($"{_selectedStats.Experience}/{_selectedStats.MaxExperience}");

        RefreshStats();
        RefreshSlots();
    }

    private void RefreshStats()
    {
        _damageValueText.SetText(_selectedStats.Damage.ToString());
        _defenceValueText.SetText(_selectedStats.Defence.ToString());
        _healthValueText.SetText($"{_selectedStats.Health}/{_selectedStats.MaxHealth}");
        _manaValueText.SetText($"{_selectedStats.Mana}/{_selectedStats.MaxMana}");
    }

    private void RefreshSlots()
    {
        foreach (var slot in _slots)
        {
            var item = _selectedStats.Equipment.Items.FirstOrDefault(x => x.SlotType == slot.SlotType);

            if (item != null)
            {
                slot.SetIcon(item.Image);
            }
            else
            {
                slot.SetDefaultIcon();
            }
        }
    }
}
