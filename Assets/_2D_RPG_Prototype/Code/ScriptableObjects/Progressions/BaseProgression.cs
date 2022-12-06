using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Progressions
{
    //[CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.PROGRESSIONS + "", order = 0)]
    public abstract class BaseProgression : ScriptableObject
    {
        public abstract float Evaluate(int level);
    }
}
