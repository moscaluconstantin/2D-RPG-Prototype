using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.InventoryItems;

namespace Assets._2D_RPG_Prototype.Code.UI.ItemsShop.ShopActionPanels
{
    public class SellPanel : ShopActionPanel
    {
        protected override void OnitemSelected(InventoryItem item)
        {
            base.OnitemSelected(item);

            PriceList = Shopkeeper.BuyPriceFor(item);

            SetPriceText(Shopkeeper.BuyPriceTextFor(item));
            CheckPrice();
        }

        protected override void OnButtonClicked()
        {
            Shopkeeper.Inventory.Remove(PriceList);
            Shopkeeper.Inventory.Add(ItemsViewer.LastSelected);
            PlayerInventory.Add(PriceList);
            PlayerInventory.Remove(ItemsViewer.LastSelected);
        }

        protected override void CheckPrice() =>
            SetButtonState(PriceList != null && Shopkeeper.Inventory.Contains(PriceList));
    }
}
