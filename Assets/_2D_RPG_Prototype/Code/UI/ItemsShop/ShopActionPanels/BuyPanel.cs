using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.ItemsShop.ShopActionPanels
{
    public class BuyPanel : ShopActionPanel
    {
        private Price[] _priceList;

        protected override void OnitemSelected(InventoryItem item)
        {
            base.OnitemSelected(item);

            _priceList = Shopkeeper.SellPriceFor(item);

            SetPriceText(Shopkeeper.SellPriceTextFor(item));
            CheckPrice();
        }

        protected override void OnButtonClicked()
        {
            PlayerInventory.Remove(_priceList);
            PlayerInventory.Add(ItemsViewer.LastSelected);
            Shopkeeper.Inventory.Remove(ItemsViewer.LastSelected);
            CheckPrice();
        }

        private void CheckPrice() =>
            SetButtonState(_priceList != null && PlayerInventory.Contains(_priceList));
    }
}
