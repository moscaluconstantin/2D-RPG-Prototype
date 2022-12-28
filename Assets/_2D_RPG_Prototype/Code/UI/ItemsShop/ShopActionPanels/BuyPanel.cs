using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;

namespace Assets._2D_RPG_Prototype.Code.UI.ItemsShop.ShopActionPanels
{
    public class BuyPanel : ShopActionPanel
    {
        protected override void OnitemSelected(InventoryItem item)
        {
            base.OnitemSelected(item);

            PriceList = Shopkeeper.SellPriceFor(item);

            SetPriceText(Shopkeeper.SellPriceTextFor(item));
            CheckPrice();
        }

        protected override void OnButtonClicked()
        {
            PlayerInventory.Remove(PriceList);
            PlayerInventory.Add(ItemsViewer.LastSelected);
            Shopkeeper.Inventory.Remove(ItemsViewer.LastSelected);
            CheckPrice();
        }

        private void CheckPrice() =>
            SetButtonState(PriceList != null && PlayerInventory.Contains(PriceList));
    }
}
