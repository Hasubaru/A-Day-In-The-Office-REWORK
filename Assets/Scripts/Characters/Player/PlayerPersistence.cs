using UnityEngine;

namespace ADayInTheOffice.Characters.Player
{
    /// <summary>
    /// Keeps the player across scenes.
    /// Attach this to PlayerRoot.
    /// </summary>
    public sealed class PlayerPersistence : MonoBehaviour
    {
        private static PlayerPersistence _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
