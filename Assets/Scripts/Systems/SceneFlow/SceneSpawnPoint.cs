using UnityEngine;

namespace ADayInTheOffice.Systems.SceneFlow
{
    /// <summary>
    /// Marks where the player should appear in a scene.
    /// </summary>
    public sealed class SceneSpawnPoint : MonoBehaviour
    {
        public static SceneSpawnPoint Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
