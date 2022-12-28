using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.ItemsShop
{
    public class MoneyPanel : MonoBehaviour
    {
        [SerializeField] private ItemCounter[] _counters;
        
        public void Initialize(IInventoryService inventory)
        {
            foreach (var counter in _counters)
                counter.Initialize(inventory);
        }
    }
}
