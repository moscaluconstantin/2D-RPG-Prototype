using Assets._2D_RPG_Prototype.Code.Constants;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Progressions
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.PROGRESSIONS + "CurveProgression", order = 0)]
    public class CurveProgression : BaseProgression
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private int _maxLevel;

        public override float Evaluate(int level) =>
            _curve.Evaluate(Clamp(level));

        private int Clamp(int level) =>
            Mathf.Clamp(level, 1, _maxLevel);
    }
}
