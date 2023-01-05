using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestCheckActions
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.QUEST_CHECK_ACTIONS + "CheckItemsInInventory", order = 0)]
    public class CheckItemsInInventory : QuestCheckAction
    {
        [SerializeField] private Price[] _itemsToCheck;

        public override bool Check() =>
            ServiceProvider.GetService<IInventoryService>().Contains(_itemsToCheck);
    }
}
