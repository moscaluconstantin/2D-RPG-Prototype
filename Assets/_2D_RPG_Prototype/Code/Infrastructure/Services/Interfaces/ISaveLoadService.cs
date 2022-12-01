using Assets._2D_RPG_Prototype.Code.Player;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public interface ISaveLoadService : IService
    {
        public PlayerData PlayerData { get; }
    }
}
