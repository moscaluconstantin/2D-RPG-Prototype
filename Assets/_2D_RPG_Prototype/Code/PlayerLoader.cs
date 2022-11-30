using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code
{
    public class PlayerLoader : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;

        public GameObject Player { get; set; }

        private void Awake()
        {
            var clones = GameObject.FindObjectsOfType<PlayerMovement>();

            if (clones.Length > 0)
            {
                for (int i = 0; i < clones.Length; i++)
                    Destroy(clones[i].gameObject);
            }

            Player = Instantiate(_playerPrefab);
        }
    }
}
