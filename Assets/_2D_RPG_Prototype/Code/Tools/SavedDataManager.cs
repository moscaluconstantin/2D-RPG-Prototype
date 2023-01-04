using UnityEditor;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Tools
{
    public static class SavedDataManager
    {
        [MenuItem("Custom Settings/Saved Data/Clear")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
