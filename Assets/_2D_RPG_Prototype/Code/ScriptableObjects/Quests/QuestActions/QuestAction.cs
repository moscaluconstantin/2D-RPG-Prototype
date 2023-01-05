using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestActions
{
    //[CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.PROGRESSIONS + "", order = 0)]
    public abstract class QuestAction : ScriptableObject
    {
        public abstract void Execute();
    }
}
