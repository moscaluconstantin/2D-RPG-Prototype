using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.ItemsShop.ShopActionPanels
{
    public class SellPanel : ShopActionPanel
    {
        private Price[] _priceList;

        protected override void OnitemSelected(InventoryItem item)
        {
            base.OnitemSelected(item);

            _priceList = Shopkeeper.BuyPriceFor(item);

            SetPriceText(Shopkeeper.BuyPriceTextFor(item));
        }

        protected override void OnButtonClicked()
        {
            Shopkeeper.Inventory.Add(ItemsViewer.LastSelected);
            PlayerInventory.Add(_priceList);
            PlayerInventory.Remove(ItemsViewer.LastSelected);
        }
    }
}
