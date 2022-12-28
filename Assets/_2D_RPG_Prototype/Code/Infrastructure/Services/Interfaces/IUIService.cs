using Assets._2D_RPG_Prototype.Code.UI;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface IUIService : IService
    {
        bool AnyWindowActive { get; }
        UIWindow ActiveWindow { get; }

        void AddWindow<T>(UIWindow window) where T : UIWindow;
        T GetWindow<T>() where T : UIWindow;
        void SetActiveWindow(UIWindow uiWindow);
        void ClearActiveWindow();
    }
}
