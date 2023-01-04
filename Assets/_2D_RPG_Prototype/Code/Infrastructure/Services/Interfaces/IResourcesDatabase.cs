using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface IResourcesDatabase : IService
    {
        InventoryItem GetInventoryItem(int id);
    }
}
