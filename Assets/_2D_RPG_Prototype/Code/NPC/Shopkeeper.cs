using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using Assets._2D_RPG_Prototype.Code.UI.ItemsShop;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public class Shopkeeper : InteractableNPC
    {
        [SerializeField] private ExchangeDetails[] _forSale;
        [SerializeField] private ExchangeDetails[] _forBuying;
        [SerializeField] private InventoryService _inventory;

        public IInventoryService Inventory => _inventory;
        public List<int> ForBuyingIds => _forBuyingIds;

        private Shop _shop;
        private bool _opened = false;
        private List<int> _forBuyingIds;

        private void Awake()
        {
            _shop = ServiceProvider.GetService<IUIService>().Shop;
            _forBuyingIds = _forBuying.Select(x => x.Item.Id).ToList();
        }

        private void Update()
        {
            if (_opened || !Triggered)
                return;

            _opened = true;
            Player.SetMovementState(false);
            _shop.Show(this);
        }

        public Price[] BuyPriceFor(InventoryItem item) =>
            GetPrice(_forBuying, item);

        public Price[] SellPriceFor(InventoryItem item) =>
            GetPrice(_forSale, item);

        public string BuyPriceTextFor(InventoryItem item) =>
            GetPriceText(_forBuying, item);

        public string SellPriceTextFor(InventoryItem item) =>
            GetPriceText(_forSale, item);

        private Price[] GetPrice(ExchangeDetails[] details, InventoryItem item)
        {
            if (!details.Any(x => x.Item.Id == item.Id))
                return null;

            return details.First(x => x.Item.Id == item.Id).PriceList;
        }

        private string GetPriceText(ExchangeDetails[] details, InventoryItem item)
        {
            if (!details.Any(x => x.Item.Id == item.Id))
                return string.Empty;

            return details.First(x => x.Item.Id == item.Id).ToString();
        }
    }
}
