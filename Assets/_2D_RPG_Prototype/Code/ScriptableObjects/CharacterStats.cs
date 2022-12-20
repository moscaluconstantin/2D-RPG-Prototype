using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Enums;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.Player;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Progressions;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.BASE_PATH + "CharacterStats", order = 0)]
    public class CharacterStats : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;

        [Header("Constant Values")]
        [SerializeField] private int _damage;

        [Header("Progressions")]
        [SerializeField] private BaseProgression _experienceProgression;
        [SerializeField] private BaseProgression _healtProgression;
        [SerializeField] private BaseProgression _manaProgression;

        public string Name => _name;
        public Sprite Image => _image;
        public int Level => _level;
        public int Experience => _experience;
        public int Health => _health;
        public int Mana => _mana;
        public int Defence => _defence + _equipment.GetStatValue(StatType.Defence);
        public int MaxExperience => Mathf.FloorToInt(_experienceProgression.Evaluate(_level));
        public int MaxHealth => Mathf.FloorToInt(_healtProgression.Evaluate(_level));
        public int MaxMana => Mathf.FloorToInt(_manaProgression.Evaluate(_level));
        public int Damage => _damage + _equipment.GetStatValue(StatType.Damage);
        public Equipment Equipment => _equipment;

        private int _level = 1;
        private int _experience;
        private int _health;
        private int _mana;
        private int _defence;
        private Equipment _equipment;

        public void Initialize(IInventoryService inventory)
        {
            _equipment = new Equipment(inventory);

            _health = MaxHealth;
            _mana = MaxMana;
            _defence = 0;
        }

        public void AddExperience(int experienceToAdd)
        {
            _experience += experienceToAdd;

            if (_experience < MaxExperience)
                return;

            LevelUp();
        }

        public void AddToCurrentValue(StatValue stat)
        {
            switch (stat.Type)
            {
                case Enums.StatType.Health:
                    _health = Mathf.FloorToInt(Mathf.Clamp(_health + stat.Value, 0, MaxHealth));
                    break;
                case Enums.StatType.Mana:
                    _mana = Mathf.FloorToInt(Mathf.Clamp(_mana + stat.Value, 0, MaxMana));
                    break;
                default: break;
            }
        }

        private void LevelUp()
        {
            _experience -= MaxExperience;
            _level++;

            _health = MaxHealth;
            _mana = MaxMana;
        }
    }
}
