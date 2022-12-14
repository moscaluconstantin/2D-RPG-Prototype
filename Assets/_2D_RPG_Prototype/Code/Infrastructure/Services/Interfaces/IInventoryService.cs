using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public interface IInventoryService : IService
    {
        InventoryItem[] Items { get; }

        int Count(InventoryItem item);
        bool Contains(InventoryItem item, int amount = 1);
        void Add(InventoryItem item, int amount = 1);
        void Remove(InventoryItem item, int amount = 1);
    }
}
