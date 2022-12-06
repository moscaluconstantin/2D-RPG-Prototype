using Assets._2D_RPG_Prototype.Code.Constants;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Progressions
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.PROGRESSIONS + "CustomProgression", order = 0)]
    public class CustomProgression : BaseProgression
    {
        [SerializeField] private float[] _values;

        public override float Evaluate(int level) =>
            _values[Clamp(level)];

        private int Clamp(int level) =>
            Mathf.Clamp(level - 1, 0, _values.Length - 1);
    }
}
