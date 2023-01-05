using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestActions
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.QUEST_ACTIONS + "Add Items", order = 0)]
    public class AddItemsAction : QuestAction
    {
        [SerializeField] Price[] _itemsToAdd;

        public override void Execute() =>
            ServiceProvider.GetService<IInventoryService>().Add(_itemsToAdd);
    }
}
