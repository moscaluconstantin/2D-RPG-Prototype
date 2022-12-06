using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Progressions
{
    public abstract class ClassicProgression : BaseProgression
    {
        [SerializeField] private int _maxLevel;
        [SerializeField] protected float BaseValue;
        [SerializeField] protected float Step;

        protected int Clamp(int level) =>
            Mathf.Clamp(level - 1, 0, _maxLevel - 1);
    }
}
