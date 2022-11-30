using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure
{
    public static class ServiceProvider
    {
        public static ICoroutineRunner CoroutineRunner { get; set; }
        public static IScreenFader ScreenFader { get; set; }
        public static ISceneLoader SceneLoader { get; set; }
    }
}
