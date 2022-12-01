using Assets._2D_RPG_Prototype.Code.UI;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface IUIService : IService
    {
        DialogueManager DialogManager { get; }
    }
}
