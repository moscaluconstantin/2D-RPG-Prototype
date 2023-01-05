using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestCheckActions
{
    //[CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.PROGRESSIONS + "", order = 0)]
    public abstract class QuestCheckAction : ScriptableObject
    {
        public abstract bool Check();
    }
}
