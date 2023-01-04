using Assets._2D_RPG_Prototype.Code.Player;
using System;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class SaveLoadService : ISaveLoadService
    {
        public event Action OnSave;
        public PlayerData PlayerData => _playerData;

        private PlayerData _playerData;

        public SaveLoadService() =>
            _playerData = PlayerData.Load();

        public void Save()
        {
            _playerData.Save();

            OnSave?.Invoke();
        }
    }
}
