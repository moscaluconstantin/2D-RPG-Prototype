using Assets._2D_RPG_Prototype.Code.Constants;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Player
{
    public class PlayerData
    {
        public string SceneName;
        public string TransitionPointKey;

        public static PlayerData Load()
        {
            if (PlayerPrefs.HasKey(SaveKeys.PLAYER_DATA))
            {
                string savedPlayerData = PlayerPrefs.GetString(SaveKeys.PLAYER_DATA, "");
                return JsonUtility.FromJson<PlayerData>(savedPlayerData);
            }

            return new PlayerData()
            {
                SceneName = GameConstants.DEFAULT_SCENE,
                TransitionPointKey = GameConstants.DEFAULT_TRANSITION_KEY
            };
        }

        public void Save() =>
            PlayerPrefs.SetString(SaveKeys.PLAYER_DATA, JsonUtility.ToJson(this));
    }
}
