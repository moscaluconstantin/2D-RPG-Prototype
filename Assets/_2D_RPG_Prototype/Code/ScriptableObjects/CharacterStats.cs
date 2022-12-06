using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Progressions;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.BASE_PATH + "CharacterStats", order = 0)]
    public class CharacterStats : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;

        [Header("Progressions")]
        [SerializeField] private BaseProgression _experienceProgression;
        [SerializeField] private BaseProgression _healtProgression;
        [SerializeField] private BaseProgression _manaProgression;
        [SerializeField] private BaseProgression _strengthProgression;

        public int ExperienceToNextLevel => Mathf.FloorToInt(_experienceProgression.Evaluate(_level));
        public int MaxHealth => Mathf.FloorToInt(_healtProgression.Evaluate(_level));
        public int MaxMana => Mathf.FloorToInt(_manaProgression.Evaluate(_level));

        private int _level = 1;
        private int _experience;
        private int _health;
        private int _mana;

        public void Initialize()
        {
            _health = MaxHealth;
            _mana = MaxMana;
        }

        public void AddExperience(int experienceToAdd)
        {
            _experience += experienceToAdd;

            if (_experience < ExperienceToNextLevel)
                return;

            LevelUp();
        }

        private void LevelUp()
        {
            _experience -= ExperienceToNextLevel;
            _level++;

            _health = MaxHealth;
            _mana = MaxMana;
        }
    }
}
