using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure
{
    public class SceneBootstraper: MonoBehaviour
    {
        private void Awake()
        {
            int scenes = SceneManager.sceneCount;

            for (int i = 0; i < scenes; i++)
            {
                if (SceneManager.GetSceneAt(i).name == "BootstrapScene")
                    return;
            }

            SceneManager.LoadScene("BootstrapScene");
        }
    }
}
