using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface IInventoryService : IService
    {
        InventoryItem[] Items { get; }

        event Action OnItemsAmountChanged;
        event Action OnItemsCountChanged;

        int Count();
        int Count(InventoryItem item);
        bool Contains(InventoryItem item, int amount = 1);
        void Add(InventoryItem item, int amount = 1);
        void Remove(InventoryItem item, int amount = 1);
    }
}
