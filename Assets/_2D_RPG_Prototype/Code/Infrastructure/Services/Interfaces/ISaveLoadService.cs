using Assets._2D_RPG_Prototype.Code.Player;
using System;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public interface ISaveLoadService : IService
    {
        event Action OnSave;

        PlayerData PlayerData { get; }

        void Save();
    }
}
