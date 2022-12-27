using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using System;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public struct ExchangeDetails
    {
        [SerializeField] private InventoryItem _item;
        [SerializeField] private Price[] _priceList;

        public InventoryItem Item => _item;
        public Price[] PriceList => _priceList;

        public override string ToString() =>
            String.Join(" ", _priceList.Select(x => x.ToString()));
    }
}
