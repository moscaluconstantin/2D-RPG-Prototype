using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestActions
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.QUEST_ACTIONS + "Remove Items", order = 0)]
    public class RemoveItemsAction : QuestAction
    {
        [SerializeField] Price[] _itemsToRemove;

        public override void Execute() =>
            ServiceProvider.GetService<IInventoryService>().Remove(_itemsToRemove);
    }
}
