using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public struct Price
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private int _count;

        public InventoryItem Item => _item;
        public int Count => _count;

        public override string ToString() =>
            $"{_count}{_item}";
    }
}
