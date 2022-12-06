using Assets._2D_RPG_Prototype.Code.Constants;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Progressions
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.PROGRESSIONS + "ArithmeticProgression", order = 0)]
    public class ArithmeticProgression : ClassicProgression
    {
        public override float Evaluate(int level) =>
            BaseValue + Step * Clamp(level);
    }
}
