using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;

namespace Assets._2D_RPG_Prototype.Code.Player
{
    public class PlayerData
    {
        public string SceneName;
        public string TransitionPointKey;

        public static PlayerData Default()
        {
            return new PlayerData()
            {
                SceneName = GameConstants.DEFAULT_SCENE,
                TransitionPointKey = GameConstants.DEFAULT_TRANSITION_KEY
            };
        }

        public static PlayerData Load() =>
            SaveLoadService.Load(SaveKeys.PLAYER_DATA, Default());

        public void Save() =>
            SaveLoadService.Save(SaveKeys.PLAYER_DATA, this);
    }
}
