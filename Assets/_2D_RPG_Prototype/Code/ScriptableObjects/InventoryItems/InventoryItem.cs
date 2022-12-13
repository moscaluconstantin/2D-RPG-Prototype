using Assets._2D_RPG_Prototype.Code.Constants;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems
{

    [CreateAssetMenu(fileName = ResourceFileNames.INVENTORY_ITEM, menuName = ResourcesMenu.INVENTORY_ITEMS + "InventoryItem", order = 0)]
    public class InventoryItem : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _image;

        public int Id => _id;
        public string Name => _name;
        public string Description => _description;
        public Sprite Image => _image;
    }
}
