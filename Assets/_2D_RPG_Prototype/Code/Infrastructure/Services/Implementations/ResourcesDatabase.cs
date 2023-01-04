using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class ResourcesDatabase : IResourcesDatabase
    {
        private InventoryItem[] _inventoryItems;

        public ResourcesDatabase() =>
            _inventoryItems = Resources.LoadAll<InventoryItem>(ResourcePaths.INVENTORY_ITEMS);

        public InventoryItem GetInventoryItem(int id) =>
            _inventoryItems.FirstOrDefault(x => x.Id == id);
    }
}
