using Assets._2D_RPG_Prototype.Code.UI;
using Assets._2D_RPG_Prototype.Code.UI.ItemsShop;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface IUIService : IService
    {
        DialogueManager DialogueManager { get; }
        Shop Shop { get; }
    }
}
