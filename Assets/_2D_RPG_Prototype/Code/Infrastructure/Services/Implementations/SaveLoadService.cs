using Assets._2D_RPG_Prototype.Code.Player;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class SaveLoadService : ISaveLoadService
    {
        public PlayerData PlayerData => _playerData;

        private PlayerData _playerData;

        public SaveLoadService()
        {
            _playerData = new PlayerData();
        }
    }
}
