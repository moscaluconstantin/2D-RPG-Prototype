using UnityEditor;
using UnityEditor.SceneManagement;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure
{
    [InitializeOnLoad]
    public static class ColdStart
    {
        private const string COLD_START_DISABLED_KEY = "cold_start_disabled";
        private const string SCENE_PATH = "Assets/_2D_RPG_Prototype/Scenes/boot.unity";

        static ColdStart()
        {
            RefreshStartScene();
        }

        [MenuItem("Custom Settings/Cold Start/Enable")]
        public static void EnableColdStart()
        {
            EditorPrefs.SetBool(COLD_START_DISABLED_KEY, false);
            RefreshStartScene();
        }

        [MenuItem("Custom Settings/Cold Start/Disable")]
        public static void DisableColdStart()
        {
            EditorPrefs.SetBool(COLD_START_DISABLED_KEY, true);
            RefreshStartScene();
        }

        [MenuItem("Custom Settings/Cold Start/Enable", true)]
        public static bool ShowEnableOption()
        {
            return !IsColdStartEnabled();
        }

        [MenuItem("Custom Settings/Cold Start/Disable", true)]
        public static bool ShowDisableOption()
        {
            return IsColdStartEnabled();
        }

        public static bool IsColdStartEnabled()
        {
            return !EditorPrefs.GetBool(COLD_START_DISABLED_KEY);
        }

        private static void RefreshStartScene()
        {
            if (IsColdStartEnabled())
            {
                EditorSceneManager.playModeStartScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(SCENE_PATH);
                return;
            }

            EditorSceneManager.playModeStartScene = null;
        }
    }
}
